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
        manager.Add<DefaultFieldIndex<StringField>>();
        manager.Add<DefaultFieldIndex<SlugField>>();
        manager.Add<DefaultFieldIndex<BoolField>>();
        manager.Add<DefaultFieldIndex<IntegerField>>();
        manager.Add<DefaultFieldIndex<FloatField>>();
        manager.Add<DefaultFieldIndex<DateTimeField>>();
        manager.Add<DefaultFieldIndex<ReferenceField>>();
        manager.Add<DefaultFieldIndex<AssetField>>();
        manager.Add<GeolocationFieldIndex>();

        return manager;
    }
}
