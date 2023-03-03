// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
            return null;
        }

        return entity.ToModel();
    }

    public async Task<IEnumerable<AssetFolder>> QueryAsync(AssetFolderQuery queryData)
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
        else
        {
            query = query.Where(x => x.Parent == null);
        }

        return query
            .ToList()
            .OrderBy(x => x.Name)            
            .Select(x => x.ToModel())
            .ToList();
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
        await AssetFolders.ReplaceOneAsync(Builders<MongoAssetFolder>.Filter.Eq(x => x.Id, folder.Id), folder.ToMongo());

    }

    public async Task DeleteAsync(AssetFolder folder)
    {
        await AssetFolders.DeleteOneAsync(Builders<MongoAssetFolder>.Filter.Eq(x => x.Id, folder.Id));
    }
}
