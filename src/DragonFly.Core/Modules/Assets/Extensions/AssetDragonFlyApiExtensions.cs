// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetDragonFlyApiExtensions
/// </summary>
public static class AssetDragonFlyApiExtensions
{
    /// <summary>
    /// Gets the asset metadata manager.
    /// </summary>
    /// <param name="api"></param>
    /// <returns></returns>
    public static AssetMetadataManager AssetMetadata(this IDragonFlyApi api)
    {
        return AssetMetadataManager.Default;
    }

    public static AssetMetadataManager AddDefaults(this AssetMetadataManager manager)
    {
        manager.Add<ImageMetadata>();
        manager.Add<PdfMetadata>();

        return manager;
    }
}
