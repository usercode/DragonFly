using DragonFly.Assets;
using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public class AssetMetadataManager
    {
        private IDictionary<string, Type> _byName;
        private IDictionary<Type, string> _byType;

        private static AssetMetadataManager? _default;

        public static AssetMetadataManager Default
        {
            get
            {
                if(_default == null)
                {
                    _default = new AssetMetadataManager();

                    _default.Register<ImageMetadata>();
                }

                return _default;
            }
        }

        private AssetMetadataManager()
        {
            _byName = new Dictionary<string, Type>();
            _byType = new Dictionary<Type, string>();
        }

        public void Register<TMetadata>()
            where TMetadata : AssetMetadata, new()
        {
            var a = typeof(TMetadata).GetCustomAttribute<AssetMetadataAttribute>();

            if(a == null)
            {
                throw new Exception($"The class '{typeof(TMetadata).Name}' needs the ImageMetadataAttribute.");
            }

            _byName.Add(a.Name, typeof(TMetadata));
            _byType.Add(typeof(TMetadata), a.Name);
        }

        public string GetMetadataName<T>()
            where T : AssetMetadata
        {
            return GetMetadataName(typeof(T));
        }

        public string GetMetadataName(Type type)
        {
            if (_byType.TryGetValue(type, out string? name))
            {
                return name;
            }

            throw new Exception();
        }

        public Type GetMetadataType(string name)
        {
            if(_byName.TryGetValue(name, out Type? result))
            {
                return result;
            }

            throw new Exception();
        }
     
    }
}
