// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class AssetMetadatasExtensions
{
    public static Asset SetMetadata(this Asset asset, AssetMetadata metadata)
    {
        string name = AssetMetadataManager.Default.GetMetadataName(metadata.GetType());

        asset.Metaddata[name] = metadata;
        
        return asset;
    }

    public static AssetMetadata GetMetadata(this Asset asset, Type type)
    {
        string name = AssetMetadataManager.Default.GetMetadataName(type);

        if (asset.Metaddata.TryGetValue(name, out AssetMetadata? metadata))
        {
            return metadata;
        }

        throw new Exception($"Metadata '{name}' not found.");
    }

    public static T GetMetadata<T>(this Asset asset)
          where T : AssetMetadata
    {
        return (T)GetMetadata(asset, typeof(T));
    }
}
