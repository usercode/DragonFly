// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Cryptography;
using DragonFly.Assets.Query;
using DragonFly.Storage;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IAssetStorage
{
    private Asset SetPreviewUrl(Asset asset)
    {
        IAssetPreviewUrlService? previewService = Api.ServiceProvider.GetService<IAssetPreviewUrlService>();

        if (previewService != null)
        {
            asset.PreviewUrl = previewService.CreateUrl(asset, 800, 800);
        }

        return asset;
    }

    public async Task<Asset> GetAssetAsync(Guid id)
    {
        MongoAsset asset = await Assets.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        return SetPreviewUrl(asset.ToModel());
    }

    public async Task CreateAsync(Asset asset)
    {
        DateTime now = DateTimeService.Current();

        asset.CreatedAt = now;
        asset.ModifiedAt = now;

        MongoAsset mongoAsset = asset.ToMongo();

        if (string.IsNullOrWhiteSpace(asset.Slug) == true)
        {
            asset.Slug = asset.Name.ToSlug();
        }

        await Assets.InsertOneAsync(mongoAsset);

        asset.Id = mongoAsset.Id;
    }

    public async Task UpdateAsync(Asset asset)
    {
        await Assets.UpdateOneAsync(
                        Builders<MongoAsset>.Filter.Eq(x => x.Id, asset.Id),
                        Builders<MongoAsset>.Update
                                                .Set(x => x.Name, asset.Name)
                                                .Set(x => x.Slug, asset.Slug.ToSlug())
                                                .Set(x => x.Alt, asset.Alt)
                                                .Set(x => x.Description, asset.Description)
                                                .Set(x => x.Folder, asset.Folder?.Id)
                                                .Set(x => x.ModifiedAt, DateTimeService.Current())
                                                .Inc(x => x.Version, 1)
                                                );
    }

    public async Task UploadAsync(Guid id, string mimetype, Stream stream)
    {
        //get asset by id
        Asset asset = await GetAssetAsync(id);
        
        //upload new stream to asset
        await AssetData.UploadFromStreamAsync(id.ToString(), stream);

        //refresh asset infos
        using (Stream s = await AssetData.OpenDownloadStreamByNameAsync(asset.Id.ToString()))
        {
            SHA256 sha = SHA256.Create();

            byte[] hash = sha.ComputeHash(s);

            string hashString = Convert.ToHexString(hash).ToLowerInvariant();

            asset.Size = s.Length;
            asset.Hash = hashString;                
            asset.MimeType = mimetype;
            asset.Metaddata.Clear();

            await Assets.UpdateOneAsync(Builders<MongoAsset>.Filter.Eq(x => x.Id, id),
                                        Builders<MongoAsset>.Update
                                                    .Set(x => x.Size, s.Length)
                                                    .Set(x => x.Hash, hashString)
                                                    .Set(x => x.MimeType, mimetype)
                                                    .Set(x => x.Metaddata, new Dictionary<string, BsonDocument>())
                                                    .Inc(x => x.Version, 1));
        }

        await ApplyMetadataAsync(asset.Id);
    }

    public async Task<Stream> DownloadAsync(Guid id)
    {
        return await AssetData.OpenDownloadStreamByNameAsync(id.ToString());
    }

    public async Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery)
    {
        IMongoQueryable<MongoAsset> query = Assets.AsQueryable();

        if (assetQuery.Folder != null)
        {
            query = query.Where(x => x.Folder == assetQuery.Folder.Value);
        }

        if (string.IsNullOrEmpty(assetQuery.Pattern) == false)
        {
            query = query.Where(x => x.Name!.Contains(assetQuery.Pattern) || x.Slug!.Contains(assetQuery.Pattern));
        }

        IList<MongoAsset> result = await query
                                            .OrderByDescending(x => x.CreatedAt)
                                            .Take(assetQuery.Take)
                                            .ToListAsync();

        QueryResult<Asset> queryResult = new QueryResult<Asset>();
        queryResult.Items = result
                                .Select(x => SetPreviewUrl(x.ToModel()))
                                .ToList();
        queryResult.Offset = assetQuery.Take;
        queryResult.Count = queryResult.Items.Count;
        queryResult.TotalCount = 0;

        return queryResult;
    }

    public async Task PublishAsync(Guid id)
    {
        Asset asset = await GetAssetAsync(id);

        //execute interceptors
        foreach (IContentInterceptor interceptor in Interceptors)
        {
            await interceptor.OnPublishedAsync(this, asset);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var fileData = await AssetData.FindAsync(Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, id.ToString()));

        foreach (GridFSFileInfo f in await fileData.ToListAsync())
        {
            await AssetData.DeleteAsync(f.Id);
        }

        await Assets.DeleteOneAsync(Builders<MongoAsset>.Filter.Eq(x => x.Id, id));
    }

    public async Task ApplyMetadataAsync(Guid id)
    {
        Asset asset = await GetAssetAsync(id);

        MongoAssetProcessingContext context = new MongoAssetProcessingContext(asset, Assets, AssetData);

        //add metadata
        foreach (IAssetProcessing processing in AssetProcessings.Where(x => x.SupportedMimetypes.Contains(asset.MimeType) ))
        {
            await processing.OnAssetChangedAsync(context);
        }
    }
}
