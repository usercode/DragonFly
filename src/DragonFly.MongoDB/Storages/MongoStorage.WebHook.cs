// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IWebHookStorage
{
    public async Task<QueryResult<WebHook>> QueryAsync(WebHookQuery query)
    {
        IMongoQueryable<MongoWebHook> q = WebHooks.AsQueryable();

        if (query.Event != null)
        {
            q = q.Where(x => x.EventName == query.Event);
        }

        var result = await q.ToListAsync();

        var items = result.Select(x => x.ToModel()).ToList();

        return new QueryResult<WebHook>() { Items = items };
    }

    public async Task<WebHook> GetAsync(Guid id)
    {
        MongoWebHook? result = await WebHooks.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        if(result == null)
        {
            throw new Exception();
        }

        return result.ToModel();
    }

    public async Task CreateAsync(WebHook webHook)
    {
        MongoWebHook m = webHook.ToMongo();

        await WebHooks.InsertOneAsync(m);

        webHook.Id = m.Id;
    }

    public async Task UpdateAsync(WebHook webHook)
    {
        await WebHooks.ReplaceOneAsync(Builders<MongoWebHook>.Filter.Eq(x=> x.Id, webHook.Id), webHook.ToMongo());
    }

    public async Task DeleteAsync(WebHook webHook)
    {
        await WebHooks.DeleteOneAsync(Builders<MongoWebHook>.Filter.Eq(x => x.Id, webHook.Id));
    }
}
