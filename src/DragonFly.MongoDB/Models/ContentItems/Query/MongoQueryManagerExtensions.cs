// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

public static class MongoQueryManagerExtensions
{
    public static MongoQueryManager MongoQuery(this IDragonFlyApi api)
    {
        return MongoQueryManager.Default;
    }

    public static MongoQueryManager AddDefaults(this MongoQueryManager manager)
    {
        manager.Register<BoolFieldQuery, BoolFieldQueryAction>();
        manager.Register<StringFieldQuery, StringFieldQueryAction>();
        manager.Register<SlugFieldQuery, SlugFieldQueryAction>();
        manager.Register<IntegerFieldQuery, IntegerFieldQueryAction>();
        manager.Register<ReferenceFieldQuery, ReferenceFieldQueryAction>();
        manager.Register<AssetFieldQuery, AssetFieldQueryAction>();
        manager.Register<GeolocationFieldQuery, GeolocationFieldQueryAction>();

        return manager;
    }
}
