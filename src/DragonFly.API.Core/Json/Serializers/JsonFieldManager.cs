// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace DragonFly.API;

/// <summary>
/// JsonFieldManager
/// </summary>
public sealed class JsonFieldManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static JsonFieldManager Default { get; } = new JsonFieldManager();

    private IDictionary<Type, IJsonFieldSerializer> _fields;

    private JsonFieldManager()
    {
        _fields = new Dictionary<Type, IJsonFieldSerializer>();
    }

    public void Add(IJsonFieldSerializer fieldSerializer)
    {
        _fields[fieldSerializer.FieldType] = fieldSerializer;
    }

    public void Add<TSerializer>()
        where TSerializer : IJsonFieldSerializer, new()
    {
        TSerializer serializer = new TSerializer();

        Add(serializer);
    }

    public IJsonFieldSerializer GetByFieldType(Type contentFieldType)
    {
        if (TryGetByFieldType(contentFieldType, out IJsonFieldSerializer? fieldSerializer))
        {
            return fieldSerializer;
        }

        throw new Exception($"No field serializer for '{contentFieldType.Name}' was found.");
    }

    public bool TryGetByFieldType(Type contentFieldType, [NotNullWhen(true)] out IJsonFieldSerializer? fieldSerializer)
    {
        return _fields.TryGetValue(contentFieldType, out fieldSerializer);
    }
}
