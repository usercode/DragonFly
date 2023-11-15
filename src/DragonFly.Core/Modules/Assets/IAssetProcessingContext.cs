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
    /// Writes metadata to the asset.
    /// </summary>
    /// <param name="metadata"></param>
    /// <returns></returns>
    Task SetMetadataAsync(AssetMetadata metadata);

    /// <summary>
    /// Opens the stream of the asset.
    /// </summary>
    /// <returns></returns>
    Task<Stream> OpenAssetStreamAsync();
}
