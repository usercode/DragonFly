using DragonFly.Core.Events.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Events;

public interface IEventStorage
{
    Task<IEnumerable<EventEntry>> QueryAsync(EventEntryQuery query);

    Task SaveAsync(EventEntry dragonFlyEvent);
}
