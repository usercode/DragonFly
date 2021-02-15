using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DragonFly.AspNetCore.Rest.Exports;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Assets;
using DragonFly.Contents.Content.Fields;
using DragonFly.Contents.Content.Parts.Base;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Assets.Queries;
using DragonFly.Core.Queries;
using DragonFly.Data.Content.ContentParts;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Data.Models;
using DragonFly.Data.Models.Assets;
using DragonFly.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace DragonFly.Data
{
    /// <summary>
    /// MongoStore
    /// </summary>
    public partial class MongoStorage : IAssetStorage
    {
        public async Task<Asset> GetAssetAsync(Guid id)
        {
            MongoAsset asset = await Assets.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            return asset.FromMongo();
        }

        public async Task CreateAsync(Asset asset)
        {
            DateTime now = DateTime.UtcNow;

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
            //asset.ModifiedAt = DateTime.UtcNow;

            //MongoAsset mongoAsset = asset.ToMongo();

            //await Assets.ReplaceOneAsync(Builders<MongoAsset>.Filter.Where(x => x.Id == asset.Id), mongoAsset);

            await Assets.UpdateOneAsync(
                            Builders<MongoAsset>.Filter.Eq(x => x.Id, asset.Id),
                            Builders<MongoAsset>.Update
                                                    .Set(x => x.Name, asset.Name)
                                                    .Set(x => x.Slug, asset.Slug.ToSlug())
                                                    .Set(x => x.Description, asset.Description)
                                                    .Set(x => x.Folder, asset.Folder?.Id)
                                                    .Set(x => x.ModifiedAt, DateTime.UtcNow)
                                                    .Inc(x => x.Version, 1)
                                                    );
        }

        public async Task UploadAsync(Guid id, string mimetype, Stream stream)
        {
            Asset asset = await GetAssetAsync(id);

            await AssetData.UploadFromStreamAsync(id.ToString(), stream);

            using (Stream s = await AssetData.OpenDownloadStreamByNameAsync(asset.Id.ToString()))
            {
                SHA256 sha = SHA256.Create();

                byte[] hash = sha.ComputeHash(s);

                string hashString = hash.Aggregate(string.Empty, (a, b) => a += b.ToString("x2"));

                //await Assets.UpdateOneAsync(Builders<MongoAsset>.Filter.Eq(x => x.Id, id),
                //                            Builders<MongoAsset>.Update
                //                                        .Set(x => x.Size, s.Length)
                //                                        .Set(x => x.Hash, hashString)
                //                                        .Inc(x => x.Version, 1));

                asset.Hash = hashString;
                asset.Size = s.Length;
                asset.MimeType = mimetype;
            }

            foreach (IAssetProcessing processing in AssetProcessings.Where(x => x.MimeTypes.Any(m => m == asset.MimeType)))
            {
                using (Stream assetStream = await AssetData.OpenDownloadStreamByNameAsync(asset.Id.ToString()))
                {
                    await processing.OnAssetChangedAsync(asset, assetStream);
                }
            }

            await Assets.FindOneAndReplaceAsync(Builders<MongoAsset>.Filter.Eq(x => x.Id, asset.Id), asset.ToMongo());
        }

        public async Task<Stream> DownloadAsync(Guid id)
        {
            return await AssetData.OpenDownloadStreamByNameAsync(id.ToString());
        }

        public async Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery)
        {
            IQueryable<MongoAsset> query = Assets.AsQueryable();

            if (assetQuery.Folder != null)
            {
                query = query.Where(x => x.Folder == assetQuery.Folder.Value);
            }

            if(string.IsNullOrEmpty(assetQuery.Pattern) == false)
            {
                query = query.Where(x => x.Name.Contains(assetQuery.Pattern));
            }

            QueryResult<Asset> queryResult = new QueryResult<Asset>();
            queryResult.Items = query
                                    .OrderByDescending(x => x.CreatedAt)
                                    .Take(assetQuery.Take)
                                    .ToList()
                                    .Select(x => x.FromMongo())
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

        public async Task DeleteAsybc(Guid id)
        {
            var fileData = await AssetData.FindAsync(Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, id.ToString()));

            foreach (var f in await fileData.ToListAsync())
            {
                await AssetData.DeleteAsync(f.Id);
            }
            await Assets.DeleteOneAsync(Builders<MongoAsset>.Filter.Eq(x => x.Id, id));
        }
    }
}
