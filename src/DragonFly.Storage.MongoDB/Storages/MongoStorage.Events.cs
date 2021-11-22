using DragonFly.Core.Events;
using DragonFly.Core.Events.Queries;
using DragonFly.Storage.MongoDB.Models.Events;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Data
{
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
}
