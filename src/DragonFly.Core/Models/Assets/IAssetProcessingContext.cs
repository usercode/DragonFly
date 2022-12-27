// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IAssetProcessingContext
/// </summary>
public interface IAssetProcessingContext
{
    /// <summary>
    /// Asset
    /// </summary>
    Asset Asset { get; }

    /// <summary>
    /// SetMetadataAsync
    /// </summary>
    /// <param name="metadata"></param>
    /// <returns></returns>
    Task SetMetadataAsync(AssetMetadata metadata);

    /// <summary>
    /// OpenAssetStreamAsync
    /// </summary>
    /// <returns></returns>
    Task<Stream> OpenAssetStreamAsync();
}
