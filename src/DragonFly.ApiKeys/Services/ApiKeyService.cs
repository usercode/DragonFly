// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.ApiKeys.AspNetCore.Storage.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.ApiKeys.AspNetCore.Services;

/// <summary>
/// ApiKeyService
/// </summary>
class ApiKeyService : IApiKeyService
{
    public ApiKeyService(MongoIdentityStore store)
    {
        Store = store;
    }

    /// <summary>
    /// Store
    /// </summary>
    private MongoIdentityStore Store { get; }

    public async Task CreateApiKey(ApiKey apiKey)
    {
        MongoApiKey mongo = apiKey.ToMongo();

        await Store.ApiKeys.InsertOneAsync(mongo);

        apiKey.Id = mongo.Id;
    }

    public async Task UpdateApiKey(ApiKey apiKey)
    {
        await Store.ApiKeys.UpdateOneAsync(
                                Builders<MongoApiKey>.Filter.Eq(x => x.Id, apiKey.Id),
                                Builders<MongoApiKey>.Update
                                                        .Set(x => x.Name, apiKey.Name)
                                                        .Set(x => x.Value, apiKey.Value)
                                                        .Set(x => x.Permissions, apiKey.Permissions));
    }

    public async Task DeleteApiKey(ApiKey apiKey)
    {
        await Store.ApiKeys.DeleteOneAsync(Builders<MongoApiKey>.Filter.Eq(x => x.Id, apiKey.Id));
    }

    public async Task<ApiKey> GetApiKey(Guid id)
    {
        var result = await Store.ApiKeys.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        return result.ToModel();
    }

    public async Task<ApiKey?> GetApiKey(string value)
    {
        MongoApiKey? result = await Store.ApiKeys.AsQueryable().FirstOrDefaultAsync(x => x.Value == value);

        if (result == null)
        {
            return null;
        }

        return result.ToModel();
    }

    public async Task<IEnumerable<ApiKey>> GetAllApiKeys()
    {
        var result = await Store.ApiKeys.AsQueryable().ToListAsync();

        return result.Select(x => x.ToModel()).ToList();
    }       
}
