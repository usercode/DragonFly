// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;
using DragonFly.Storage.Abstractions;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoFieldManager
/// </summary>
public sealed class MongoFieldManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static MongoFieldManager Default { get; } = new MongoFieldManager();

    private IDictionary<Type, IMongoFieldSerializer> _fields = new Dictionary<Type, IMongoFieldSerializer>();

    public void Add(IMongoFieldSerializer fieldSerializer)
    {
        _fields[fieldSerializer.FieldType] = fieldSerializer;
    }

    public void Add<TSerializer>()
        where TSerializer : IMongoFieldSerializer, new()
    {
        TSerializer serializer = new TSerializer();

        Add(serializer);
    }

    public IMongoFieldSerializer GetByFieldType(Type contentFieldType)
    {
        if (TryGetByFieldType(contentFieldType, out IMongoFieldSerializer? fieldSerializer))
        {
            return fieldSerializer;
        }

        throw new Exception($"No field serializer for '{contentFieldType.Name}' was found.");
    }

    public bool TryGetByFieldType(Type contentFieldType, [NotNullWhen(true)] out IMongoFieldSerializer? fieldSerializer)
    {
        return _fields.TryGetValue(contentFieldType, out fieldSerializer);
    }
}
