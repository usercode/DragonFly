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
    /// Gets the metadata manager.
    /// </summary>
    /// <param name="api"></param>
    /// <returns></returns>
    public static MetadataManager Metadata(this IDragonFlyApi api)
    {
        return MetadataManager.Default;
    }

    public static MetadataManager AddDefaults(this MetadataManager manager)
    {
        manager.Add<ImageMetadata>();
        manager.Add<PdfMetadata>();
        manager.Add<VideoMetadata>();

        return manager;
    }
}
