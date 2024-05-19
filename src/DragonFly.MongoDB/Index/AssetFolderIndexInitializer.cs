// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using DragonFly.MongoDB.Storages;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// AssetFolderIndexInitializer
/// </summary>
class AssetFolderIndexInitializer : IPostInitialize
{
    public AssetFolderIndexInitializer(MongoClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Storage
    /// </summary>
    public MongoClient Client { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        IMongoCollection<MongoAssetFolder> assetFolders = Client.Database.GetAssetFolderCollection();

        //AssetFolders
        await assetFolders.AddIndexAsync(x => x.Name);
        await assetFolders.AddIndexAsync(x => x.Parent);
        await assetFolders.AddIndexAsync(x => x.CreatedAt);
        await assetFolders.AddIndexAsync(x => x.ModifiedAt);
    }
}
