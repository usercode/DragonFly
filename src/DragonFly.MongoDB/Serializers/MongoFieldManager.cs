// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;
using DragonFly.Storage.Abstractions;
using DragonFly.Storage.MongoDB.Fields;

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

    private IDictionary<Type, IMongoFieldSerializer> _fields;

    private MongoFieldManager()
    {
        _fields = new Dictionary<Type, IMongoFieldSerializer>();
    }

    public void RegisterField(IMongoFieldSerializer fieldSerializer)
    {
        if (_fields.TryAdd(fieldSerializer.FieldType, fieldSerializer) == false)
        {
            _fields[fieldSerializer.FieldType] = fieldSerializer;
        }
    }

    public void RegisterField<TSerializer>()
        where TSerializer : IMongoFieldSerializer, new()
    {
        TSerializer serializer = new TSerializer();

        RegisterField(serializer);
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
