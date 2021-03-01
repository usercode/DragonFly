using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DragonFly.Content;
using DragonFly.Contents.Assets;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Assets.Queries;
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
    public partial class MongoStorage : IAssetFolderStorage
    {
        public async Task<AssetFolder> GetAssetFolderAsync(Guid id)
        {
            var entity = AssetFolders.AsQueryable().FirstOrDefault(x => x.Id == id);

            return entity.ToModel();
        }

        public async Task<IEnumerable<AssetFolder>> GetAssetFoldersAsync(AssetFolderQuery queryData)
        {
            var query = AssetFolders.AsQueryable();

            if(queryData.RootOnly == true)
            {
                query = query.Where(x => x.Parent == null);
            }

            if (queryData.Parent != null)
            {
                query = query.Where(x => x.Parent == queryData.Parent.Value);
            }

            return query.OrderBy(x=> x.Name).ToList().Select(x => x.ToModel());
        }

        public async Task CreateAsync(AssetFolder folder)
        {
            folder.CreatedAt = DateTimeService.Current();
            folder.ModifiedAt = folder.CreatedAt;

            var mongo = folder.ToMongo();

            await AssetFolders.InsertOneAsync(mongo);

            folder.Id = mongo.Id;
        }

        public async Task UpdateAsync(AssetFolder folder)
        {
            await AssetFolders.ReplaceOneAsync(Builders<MongoAssetFolder>.Filter.Where(x => x.Id == folder.Id), folder.ToMongo());

        }
    }
}
