// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB.Storages;

public abstract class MongoStorage(MongoClient client)
{

    /// <summary>
    /// Client
    /// </summary>
    protected MongoClient Client { get; } = client;
}
