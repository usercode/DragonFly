using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Assets.Query;
using DragonFly.Storage;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IAssetFolderStorage
{
    public async Task<AssetFolder> GetAssetFolderAsync(Guid id)
    {
        MongoAssetFolder? entity = AssetFolders.AsQueryable().FirstOrDefault(x => x.Id == id);

        if (entity == null)
        {
            throw new Exception();
        }

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

        MongoAssetFolder mongo = folder.ToMongo();

        await AssetFolders.InsertOneAsync(mongo);

        folder.Id = mongo.Id;
    }

    public async Task UpdateAsync(AssetFolder folder)
    {
        await AssetFolders.ReplaceOneAsync(Builders<MongoAssetFolder>.Filter.Where(x => x.Id == folder.Id), folder.ToMongo());

    }
}
