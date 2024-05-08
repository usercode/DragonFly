// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Storages;
using DragonFly.Query;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStorage
/// </summary>
public class EventMongoStorage : MongoStorage, IEventStorage
{
    public EventMongoStorage(MongoClient client)
        : base(client)
    {
        Events = Client.Database.GetCollection<MongoEvent>("Events");
    }

    /// <summary>
    /// Events
    /// </summary>
    public IMongoCollection<MongoEvent> Events { get; }

    public async Task<IEnumerable<EventEntry>> QueryAsync(EventEntryQuery query)
    {
        var result = await Events.AsQueryable()
                                    .OrderByDescending(x => x.Date)
                                    .Take(100)
                                    .ToListAsync();

        return result.Select(x => x.ToModel()).ToList();
    }

    public async Task SaveAsync(EventEntry dragonFlyEvent)
    {
        await Events.InsertOneAsync(dragonFlyEvent.ToMongo());
    }
}
