// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.Events.Queries;

namespace DragonFly.Core.Events;

public interface IEventStorage
{
    Task<IEnumerable<EventEntry>> QueryAsync(EventEntryQuery query);

    Task SaveAsync(EventEntry dragonFlyEvent);
}
