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
        await assets.Indexes.DropAllAsync();

        await assets.AddIndexAsync($"{nameof(MongoAsset.Name)}");
        await assets.AddIndexAsync($"{nameof(MongoAsset.Slug)}");
        await assets.AddIndexAsync($"{nameof(MongoAsset.Alt)}");
        await assets.AddIndexAsync($"{nameof(MongoAsset.MimeType)}");
        await assets.AddIndexAsync($"{nameof(MongoAsset.Size)}");
        await assets.AddIndexAsync($"{nameof(MongoAsset.CreatedAt)}");
        await assets.AddIndexAsync($"{nameof(MongoAsset.ModifiedAt)}");
    }
}
