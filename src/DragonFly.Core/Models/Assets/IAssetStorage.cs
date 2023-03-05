// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using DragonFly.Query;

namespace DragonFly;

/// <summary>
/// IAssetStorage
/// </summary>
public interface IAssetStorage
{
    Task<QueryResult<Asset>> QueryAsync(AssetQuery assetQuery);

    Task<Asset?> GetAssetAsync(Guid id);

    Task CreateAsync(Asset asset);

    Task UpdateAsync(Asset asset);

    Task DeleteAsync(Asset asset);

    Task PublishAsync(Asset asset);

    Task UploadAsync(Asset asset, string mimetype, Stream stream);

    Task<Stream> GetStreamAsync(Asset asset);

    Task ApplyMetadataAsync(Asset asset);
}
