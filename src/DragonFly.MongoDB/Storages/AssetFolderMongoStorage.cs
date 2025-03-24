// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Storages;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartResults;

namespace DragonFly.MongoDB;

/// <summary>
/// AssetFolderMongoStorage
/// </summary>
public class AssetFolderMongoStorage : MongoStorage, IAssetFolderStorage
{
    public AssetFolderMongoStorage(
                        MongoClient client,
                        IAssetStorage assetStorage,
                        IDateTimeService dateTimeService)
                        : base(client)
    {
        AssetStorage = assetStorage;
        DateTimeService = dateTimeService;

        AssetFolders = Client.Database.GetAssetFolderCollection();
    }

    /// <summary>
    /// AssetStorage
    /// </summary>
    private IAssetStorage AssetStorage { get; }

    /// <summary>
    /// DateTimeService
    /// </summary>
    private IDateTimeService DateTimeService { get; }

    /// <summary>
    /// AssetFolders
    /// </summary>
    private IMongoCollection<MongoAssetFolder> AssetFolders { get; }

    public async Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id)
    {
        MongoAssetFolder? entity = await AssetFolders.AsQueryable().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        if (entity == null)
        {
            return null;
        }

        return entity.ToModel();
    }

    public async Task<Result<QueryResult<AssetFolder>>> QueryAsync(AssetFolderQuery queryData)
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

        IList<MongoAssetFolder> resultMongo = await query.ToListAsync().ConfigureAwait(false);

        var result = resultMongo
                            .OrderBy(x => x.Name)            
                            .Select(x => x.ToModel())
                            .ToList();

        return new QueryResult<AssetFolder>() { Offset = queryData.Skip, Count = result.Count, Items = result };
    }

    public async Task<Result> CreateAsync(AssetFolder folder)
    {
        folder.CreatedAt = DateTimeService.Current();
        folder.ModifiedAt = folder.CreatedAt;

        MongoAssetFolder mongo = folder.ToMongo();

        await AssetFolders.InsertOneAsync(mongo).ConfigureAwait(false);

        folder.Id = mongo.Id;

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(AssetFolder folder)
    {
        await AssetFolders.ReplaceOneAsync(Builders<MongoAssetFolder>.Filter.Eq(x => x.Id, folder.Id), folder.ToMongo()).ConfigureAwait(false);

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(AssetFolder folder)
    {
        //Delete sub folders
        QueryResult<AssetFolder> subFolders = await QueryAsync(new AssetFolderQuery() { Parent = folder.Id }).ConfigureAwait(false);

        foreach (AssetFolder subFolder in subFolders.Items)
        {
            await DeleteAsync(subFolder).ConfigureAwait(false);
        }

        //Delete all assets
        while (true)
        {
            QueryResult<Asset> assets = await AssetStorage.QueryAsync(new AssetQuery() { Folder = folder.Id });

            if (assets.Count == 0)
            {
                break;
            }

            foreach (Asset asset in assets.Items)
            {
                await AssetStorage.DeleteAsync(asset).ConfigureAwait(false);
            }
        }
        
        //Delete folder
        await AssetFolders.DeleteOneAsync(Builders<MongoAssetFolder>.Filter.Eq(x => x.Id, folder.Id)).ConfigureAwait(false);

        return Result.Ok();
    }
}
