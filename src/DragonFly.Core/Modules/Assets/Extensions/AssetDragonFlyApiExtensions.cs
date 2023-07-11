// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetDragonFlyApiExtensions
/// </summary>
public static class AssetDragonFlyApiExtensions
{
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
