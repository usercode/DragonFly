// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.Events;

namespace DragonFly.MongoDB;

static class MongoEventExtensions
{
    public static MongoEvent ToMongo(this EventEntry dragoonFlyEvent)
    {
        MongoEvent mongoEvent = new MongoEvent();
        mongoEvent.Id = dragoonFlyEvent.Id;
        mongoEvent.Date = dragoonFlyEvent.Date;
        mongoEvent.Name = dragoonFlyEvent.Name;

        return mongoEvent;
    }

    public static EventEntry ToModel(this MongoEvent mongoEvent)
    {
        EventEntry entity = new EventEntry(mongoEvent.Name, "");
        entity.Id = mongoEvent.Id;
        entity.Date = mongoEvent.Date;

        return entity;
    }
}
