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
        manager.Add<BoolFieldQuery, BoolFieldQueryAction>();
        manager.Add<StringFieldQuery, StringFieldQueryAction>();
        manager.Add<SlugFieldQuery, SlugFieldQueryAction>();
        manager.Add<IntegerFieldQuery, IntegerFieldQueryAction>();
        manager.Add<ReferenceFieldQuery, ReferenceFieldQueryAction>();
        manager.Add<AssetFieldQuery, AssetFieldQueryAction>();
        manager.Add<GeolocationFieldQuery, GeolocationFieldQueryAction>();

        return manager;
    }
}
