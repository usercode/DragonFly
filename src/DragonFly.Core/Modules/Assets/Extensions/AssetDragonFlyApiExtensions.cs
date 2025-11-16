// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetDragonFlyApiExtensions
/// </summary>
public static class AssetDragonFlyApiExtensions
{
    extension(IDragonFlyApi api)
    {
        public MetadataManager Metadatas => MetadataManager.Default;
    }

    public static MetadataManager AddDefaults(this MetadataManager manager)
    {
        manager.Add<ImageMetadata>();
        manager.Add<PdfMetadata>();
        manager.Add<VideoMetadata>();

        return manager;
    }
}
