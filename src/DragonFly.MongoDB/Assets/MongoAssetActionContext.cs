// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets;
using MongoDB.Driver.GridFS;
using MongoDB.Driver;

namespace DragonFly.MongoDB.Assets;
internal class MongoAssetActionContext : IAssetActionContext
{
    public MongoAssetActionContext(
                        Asset asset,
                        IGridFSBucket assetData)
    {
        Asset = asset;
        AssetData = assetData;
    }

    /// <summary>
    /// Asset
    /// </summary>
    public Asset Asset { get; }

    /// <summary>
    /// AssetData
    /// </summary>
    private IGridFSBucket AssetData { get; }

    public async Task<Stream> OpenReadAsync()
    {
        return await AssetData.OpenDownloadStreamByNameAsync(Asset.Id.ToString()).ConfigureAwait(false);
    }

    public async Task<Stream> OpenWriteAsync()
    {
        return await AssetData.OpenUploadStreamAsync(Asset.Id.ToString()).ConfigureAwait(false);
    }
}
