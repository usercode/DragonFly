// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Index;

namespace DragonFly;

public static class DragonFlyApiExtensions
{
    public static MongoIndexManager Index(this IDragonFlyApi api)
    {
        return MongoIndexManager.Default;
    }
}
