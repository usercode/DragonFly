// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetMetadataManager
/// </summary>
public sealed class AssetMetadataManager
{
    private IDictionary<string, Type> _byName = new Dictionary<string, Type>();
    private IDictionary<Type, string> _byType = new Dictionary<Type, string>();

    /// <summary>
    /// Default
    /// </summary>
    public static AssetMetadataManager Default { get; } = new AssetMetadataManager();

    public AssetMetadataAdded Add<TMetadata>()
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

        return new AssetMetadataAdded(typeof(TMetadata));
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
        if (_byName.TryGetValue(name, out Type? result))
        {
            return result;
        }

        throw new Exception();
    }
}
