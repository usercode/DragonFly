using DragonFly.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// AssetMetadataManager
/// </summary>
public class AssetMetadataManager
{
    private IDictionary<string, Type> _byName;
    private IDictionary<Type, string> _byType;

    private static AssetMetadataManager? _default;

    public static AssetMetadataManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new AssetMetadataManager();
            }

            return _default;
        }
    }

    private AssetMetadataManager()
    {
        _byName = new Dictionary<string, Type>();
        _byType = new Dictionary<Type, string>();
    }

    public void Add<TMetadata>()
        where TMetadata : AssetMetadata, new()
    {
        string typeName = typeof(TMetadata).Name;

        string suffixMetadata = "Metadata";

        if (typeName.EndsWith(suffixMetadata))
        {
            typeName = typeName[0..^suffixMetadata.Length];
        }

        _byName[typeName] = typeof(TMetadata);
        _byType[typeof(TMetadata)] = typeName;
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
