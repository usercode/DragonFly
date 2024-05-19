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
        await assets.AddIndexAsync(x => x.Name);
        await assets.AddIndexAsync(x => x.Slug);
        await assets.AddIndexAsync(x => x.Alt);
        await assets.AddIndexAsync(x => x.MimeType);
        await assets.AddIndexAsync(x => x.Size);
        await assets.AddIndexAsync(x => x.CreatedAt);
        await assets.AddIndexAsync(x => x.ModifiedAt);
    }
}
