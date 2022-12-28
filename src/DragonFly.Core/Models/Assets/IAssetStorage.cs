// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;

namespace DragonFly;

/// <summary>
/// IAssetStorage
/// </summary>
public interface IAssetStorage
{
    Task<Asset> GetAssetAsync(Guid id);

    Task<QueryResult<Asset>> GetAssetsAsync(AssetQuery assetQuery);

    Task CreateAsync(Asset asset);

    Task UpdateAsync(Asset asset);

    Task DeleteAsync(Guid id);

    Task PublishAsync(Guid id);

    Task UploadAsync(Guid id, string mimetype, Stream stream);

    Task<Stream> GetStreamAsync(Guid id);

    Task ApplyMetadataAsync(Guid id);
}
