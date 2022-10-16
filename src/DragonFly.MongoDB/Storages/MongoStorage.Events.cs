// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.Events;
using DragonFly.Core.Events.Queries;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStorage
/// </summary>
public partial class MongoStorage : IEventStorage
{
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
