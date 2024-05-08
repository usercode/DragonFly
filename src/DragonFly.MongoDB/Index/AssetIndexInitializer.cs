// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using DragonFly.MongoDB.Storages;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// CreateIndexInitializer
/// </summary>
class AssetIndexInitializer : IPostInitialize
{
    public AssetIndexInitializer(MongoClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Storage
    /// </summary>
    public MongoClient Client { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        IMongoCollection<MongoAsset> assets = Client.Database.GetAssetCollection();

        //assets
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Name)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Slug)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Alt)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.MimeType)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Size)));

        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Descending(x => x.Name)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Descending(x => x.Slug)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Descending(x => x.Alt)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Descending(x => x.MimeType)));
        await assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Descending(x => x.Size)));
    }
}
