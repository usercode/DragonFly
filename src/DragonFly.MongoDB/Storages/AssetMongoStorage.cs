// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Cryptography;
using DragonFly.AspNetCore;
using DragonFly.MongoDB.Storages;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using SmartResults;

namespace DragonFly.MongoDB;

/// <summary>
/// AssetMongoStorage
/// </summary>
public class AssetMongoStorage : MongoStorage, IAssetStorage
{
    public AssetMongoStorage(
                        MongoClient client, 
                        IDragonFlyApi api,
                        IDateTimeService dateTimeService,
                        ISlugService slugService,
                        IBackgroundTaskManager backgroundTaskManager)
        : base(client)
    {
        Api = api;
        DateTimeService = dateTimeService;
        SlugService = slugService;
        BackgroundTaskManager = backgroundTaskManager;

        Assets = Client.Database.GetAssetCollection();
        AssetData = new GridFSBucket(Client.Database, new GridFSBucketOptions() { BucketName = "Assets" });
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// DateTimeService
    /// </summary>
    private IDateTimeService DateTimeService { get; }

    /// <summary>
    /// SlugService
    /// </summary>
    private ISlugService SlugService { get; }

    /// <summary>
    /// BackgroundTaskManager
    /// </summary>
    private IBackgroundTaskManager BackgroundTaskManager { get; }

    /// <summary>
    /// Assets
    /// </summary>
    public IMongoCollection<MongoAsset> Assets { get; }

    /// <summary>
    /// AssetData
    /// </summary>
    public IGridFSBucket AssetData { get; }

    private Asset SetPreviewUrl(Asset asset)
    {
        IAssetPreviewUrlService? previewService = Api.ServiceProvider.GetService<IAssetPreviewUrlService>();

        if (previewService != null)
        {
            asset.PreviewUrl = previewService.CreateUrl(asset, 800, 800);
        }

        return asset;
    }

    public async Task<Result<Asset?>> GetAssetAsync(Guid id)
    {
        MongoAsset asset = await Assets.AsQueryable().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        if (asset == null)
        {
            return Result.Ok<Asset?>();
        }

        return SetPreviewUrl(asset.ToModel());
    }

    public async Task<Result> CreateAsync(Asset asset)
    {
        DateTime now = DateTimeService.Current();

        asset.CreatedAt = now;
        asset.ModifiedAt = now;

        MongoAsset mongoAsset = asset.ToMongo();

        if (string.IsNullOrWhiteSpace(asset.Slug) == true)
        {
            asset.Slug = SlugService.Transform(asset.Name);
        }

        await Assets.InsertOneAsync(mongoAsset).ConfigureAwait(false);

        asset.Id = mongoAsset.Id;

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(Asset asset)
    {
        await Assets.UpdateOneAsync(
                        Builders<MongoAsset>.Filter.Eq(x => x.Id, asset.Id),
                        Builders<MongoAsset>.Update
                                                .Set(x => x.Name, asset.Name)
                                                .Set(x => x.Slug, SlugService.Transform(asset.Slug))
                                                .Set(x => x.Alt, asset.Alt)
                                                .Set(x => x.Description, asset.Description)
                                                .Set(x => x.Folder, asset.Folder?.Id)
                                                .Set(x => x.ModifiedAt, DateTimeService.Current())
                                                .Inc(x => x.Version, 1)
                                                ).ConfigureAwait(false);

        return Result.Ok();
    }

    public async Task<Result> UploadAsync(Guid assetId, string mimetype, Stream stream)
    {        
        //upload new stream to asset
        await AssetData.UploadFromStreamAsync(assetId.ToString(), stream).ConfigureAwait(false);

        Asset? asset = null;

        //refresh asset info
        using (Stream s = await AssetData.OpenDownloadStreamByNameAsync(assetId.ToString()).ConfigureAwait(false))
        {
            byte[] hash = await SHA256.HashDataAsync(s).ConfigureAwait(false);

            string hashString = Convert.ToHexString(hash).ToLowerInvariant();

            var mongoAsset = await Assets.FindOneAndUpdateAsync(Builders<MongoAsset>.Filter.Eq(x => x.Id, assetId),
                                        Builders<MongoAsset>.Update
                                                    .Set(x => x.Size, s.Length)
                                                    .Set(x => x.Hash, hashString)
                                                    .Set(x => x.MimeType, mimetype)
                                                    .Set(x => x.Metaddata, new Dictionary<string, BsonDocument>())
                                                    .Inc(x => x.Version, 1), 
                                        new FindOneAndUpdateOptions<MongoAsset>() { ReturnDocument = ReturnDocument.After }).ConfigureAwait(false);

            asset = mongoAsset.ToModel();
        }

        await ApplyMetadataAsync(asset).ConfigureAwait(false);

        return Result.Ok();
    }

    public async Task<Result<Stream>> OpenStreamAsync(Guid assetId)
    {
        return await AssetData.OpenDownloadStreamByNameAsync(assetId.ToString()).ConfigureAwait(false);
    }

    public async Task<Result<QueryResult<Asset>>> QueryAsync(AssetQuery assetQuery)
    {
        IQueryable<MongoAsset> query = Assets.AsQueryable();

        if (assetQuery.Folder != null)
        {
            query = query.Where(x => x.Folder == assetQuery.Folder);
        }

        if (string.IsNullOrEmpty(assetQuery.Pattern) == false)
        {
            query = query.Where(x => x.Name!.Contains(assetQuery.Pattern) || x.Slug!.Contains(assetQuery.Pattern));
        }

        if (assetQuery.Folder == null)
        {
            query = query.OrderByDescending(x => x.CreatedAt);
        }
        else
        {
            query = query.OrderBy(x => x.Name);
        }

        query = query.Skip(assetQuery.Skip);
        query = query.Take(assetQuery.Take);

        IList<MongoAsset> result = await query.ToListAsync().ConfigureAwait(false);

        QueryResult<Asset> queryResult = new QueryResult<Asset>();
        queryResult.Items = result
                                .Select(x => SetPreviewUrl(x.ToModel()))
                                .ToList();
        queryResult.Offset = assetQuery.Take;
        queryResult.Count = queryResult.Items.Count;
        queryResult.TotalCount = queryResult.Items.Count;

        return queryResult;
    }

    public async Task<Result> PublishAsync(Asset asset)
    {
        IEnumerable<IContentInterceptor> interceptors = Api.ServiceProvider.GetServices<IContentInterceptor>();

        //execute interceptors
        foreach (IContentInterceptor interceptor in interceptors)
        {
            await interceptor.OnPublishedAsync(asset).ConfigureAwait(false);
        }

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(Asset asset)
    {
        var fileData = await AssetData.FindAsync(Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, asset.Id.ToString())).ConfigureAwait(false);

        foreach (GridFSFileInfo f in await fileData.ToListAsync().ConfigureAwait(false))
        {
            await AssetData.DeleteAsync(f.Id).ConfigureAwait(false);
        }

        var result = await Assets.DeleteOneAsync(Builders<MongoAsset>.Filter.Eq(x => x.Id, asset.Id)).ConfigureAwait(false);

        return Result.Ok();
    }

    public async Task<Result> ApplyMetadataAsync(Asset asset)
    {
        MongoAssetProcessingContext context = new MongoAssetProcessingContext(asset, Assets, AssetData);

        IEnumerable<IAssetProcessing> processings = Api.ServiceProvider.GetServices<IAssetProcessing>();

        //add metadata
        foreach (IAssetProcessing processing in processings.Where(x => x.CanUse(asset.MimeType)))
        {
            await processing.OnAssetChangedAsync(context).ConfigureAwait(false);
        }

        return Result.Ok();
    }

    public Task<Result<BackgroundTaskInfo>> ApplyMetadataAsync(AssetQuery query)
    {
        BackgroundTask task = BackgroundTaskManager.Start("Apply metadata to assets", query, static async ctx =>
        {
            IAssetStorage assetStorage = ctx.ServiceProvider.GetRequiredService<IAssetStorage>();

            await ctx.ProcessQueryAsync(
                                assetStorage.QueryAsync,
                                assetStorage.ApplyMetadataAsync)
                        .ConfigureAwait(false);
        });

        return Task.FromResult<Result<BackgroundTaskInfo>>((BackgroundTaskInfo)task);
    }
}
