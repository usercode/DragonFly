// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Results;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IWebHookStorage
{
    public async Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query)
    {
        IMongoQueryable<MongoWebHook> q = WebHooks.AsQueryable();

        if (query.Event != null)
        {
            q = q.Where(x => x.EventName == query.Event);
        }

        IList<MongoWebHook> result = await q.ToListAsync();

        var items = result.Select(x => x.ToModel()).ToList();

        return new QueryResult<WebHook>() { Count = items.Count, TotalCount = items.Count, Items = items };
    }

    public async Task<Result<WebHook?>> GetAsync(Guid id)
    {
        MongoWebHook? result = await WebHooks.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        if(result == null)
        {
            return Result.Ok();
        }

        return result.ToModel();
    }

    public async Task<Result> CreateAsync(WebHook webHook)
    {
        MongoWebHook m = webHook.ToMongo();

        await WebHooks.InsertOneAsync(m);

        webHook.Id = m.Id;

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(WebHook webHook)
    {
        await WebHooks.ReplaceOneAsync(Builders<MongoWebHook>.Filter.Eq(x=> x.Id, webHook.Id), webHook.ToMongo());

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(WebHook webHook)
    {
        await WebHooks.DeleteOneAsync(Builders<MongoWebHook>.Filter.Eq(x => x.Id, webHook.Id));

        return Result.Ok();
    }
}
