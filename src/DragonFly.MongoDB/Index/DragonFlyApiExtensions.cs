// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.MongoDB;

namespace DragonFly;

public static class DragonFlyApiExtensions
{
    public static MongoIndexManager MongoIndex(this IDragonFlyApi api)
    {
        return MongoIndexManager.Default;
    }
}
