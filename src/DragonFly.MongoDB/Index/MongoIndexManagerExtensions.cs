// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Index;

namespace DragonFly.MongoDB;

public static class MongoIndexManagerExtensions
{
    public static MongoIndexManager MongoIndex(this IDragonFlyApi api)
    {
        return MongoIndexManager.Default;
    }

    public static MongoIndexManager AddDefaults(this MongoIndexManager manager)
    {
        manager.Register<DefaultFieldIndex<StringField>>();
        manager.Register<DefaultFieldIndex<SlugField>>();
        manager.Register<DefaultFieldIndex<BoolField>>();
        manager.Register<DefaultFieldIndex<IntegerField>>();
        manager.Register<DefaultFieldIndex<FloatField>>();
        manager.Register<DefaultFieldIndex<DateTimeField>>();
        manager.Register<GeolocationFieldIndex>();

        return manager;
    }
}
