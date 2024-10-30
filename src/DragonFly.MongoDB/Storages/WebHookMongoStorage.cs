// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Storages;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartResults;

namespace DragonFly.MongoDB;

/// <summary>
/// WebHookMongoStorage
/// </summary>
public class WebHookMongoStorage : MongoStorage, IWebHookStorage
{
    public WebHookMongoStorage(MongoClient client)
        : base(client)
    {
        WebHooks = Client.Database.GetCollection<MongoWebHook>("WebHooks");
    }

    /// <summary>
    /// WebHooks
    /// </summary>
    private IMongoCollection<MongoWebHook> WebHooks { get; }

    public async Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query)
    {
        IQueryable<MongoWebHook> q = WebHooks.AsQueryable();

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

        if (result == null)
        {
            return Result.Ok<WebHook?>();
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
