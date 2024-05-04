﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartResults;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IAssetFolderStorage
{
    public async Task<Result<AssetFolder?>> GetAssetFolderAsync(Guid id)
    {
        MongoAssetFolder? entity = await AssetFolders.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

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

        IList<MongoAssetFolder> resultMongo = await query.ToListAsync();

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

        await AssetFolders.InsertOneAsync(mongo);

        folder.Id = mongo.Id;

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(AssetFolder folder)
    {
        await AssetFolders.ReplaceOneAsync(Builders<MongoAssetFolder>.Filter.Eq(x => x.Id, folder.Id), folder.ToMongo());

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(AssetFolder folder)
    {
        //Delete sub folders
        QueryResult<AssetFolder> subFolders = await QueryAsync(new AssetFolderQuery() { Parent = folder.Id });

        foreach (AssetFolder subFolder in subFolders.Items)
        {
            await DeleteAsync(subFolder);
        }

        //Delete all assets
        while (true)
        {
            QueryResult<Asset> assets = await QueryAsync(new AssetQuery() { Folder = folder.Id });

            if (assets.Count == 0)
            {
                break;
            }

            foreach (Asset asset in assets.Items)
            {
                await DeleteAsync(asset);
            }
        }
        
        //Delete folder
        await AssetFolders.DeleteOneAsync(Builders<MongoAssetFolder>.Filter.Eq(x => x.Id, folder.Id));

        return Result.Ok();
    }
}
