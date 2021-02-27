using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public static class AssetMetadatasExtensions
    {
        public static Asset SetMetadata(this Asset asset, AssetMetadata metadata)
        {
            string name = AssetMetadataManager.Default.GetMetadataName(metadata.GetType());

            if (asset.Metaddata.TryAdd(name, metadata) == false)
            {
                asset.Metaddata[name] = metadata;
            }

            return asset;
        }

        public static AssetMetadata GetMetadata(this Asset asset, Type type)
        {
            if (asset.Metaddata.TryGetValue(AssetMetadataManager.Default.GetMetadataName(type), out AssetMetadata metadata))
            {
                return metadata;
            }

            throw new Exception();
        }

        public static T GetMetadata<T>(this Asset asset)
              where T : AssetMetadata
        {
            return (T)GetMetadata(asset, typeof(T));
        }
    }
}
