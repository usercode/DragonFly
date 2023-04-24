// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

public interface IEventStorage
{
    Task<IEnumerable<EventEntry>> QueryAsync(EventEntryQuery query);

    Task SaveAsync(EventEntry dragonFlyEvent);
}
