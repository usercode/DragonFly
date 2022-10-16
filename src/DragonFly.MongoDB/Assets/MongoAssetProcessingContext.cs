// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoAssetProcessingContext
/// </summary>
public class MongoAssetProcessingContext : IAssetProcessingContext
{
    public MongoAssetProcessingContext(
        Asset asset,
        IMongoCollection<MongoAsset> assets,
        IGridFSBucket assetData)
    {
        Asset = asset;
        Assets = assets;
        AssetData = assetData;
    }

    /// <summary>
    /// Asset
    /// </summary>
    public Asset Asset { get; }
    private IMongoCollection<MongoAsset> Assets { get; }
    private IGridFSBucket AssetData { get; }

    public async Task SetMetadataAsync(AssetMetadata metadata)
    {
        await Assets.UpdateOneAsync(
                                    Builders<MongoAsset>.Filter.Eq(x => x.Id, Asset.Id),
                                    Builders<MongoAsset>.Update.Set($"{nameof(MongoAsset.Metaddata)}.{AssetMetadataManager.Default.GetMetadataName(metadata.GetType())}", metadata.ToMongo()));
    }

    public async Task<Stream> OpenAssetStreamAsync()
    {
        return await AssetData.OpenDownloadStreamByNameAsync(Asset.Id.ToString());
    }
}
