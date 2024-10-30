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

    private Dictionary<Type, IMongoFieldSerializer> _fields = [];

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

    /// <summary>
    /// Ensures that the content field has a <see cref="SingleValueMongoFieldSerializer{T}"/>  or <see cref="DefaultMongoFieldSerializer{T}"/>.
    /// </summary>
    public void EnsureField(FieldFactory fieldFactory)
    {
        if (_fields.ContainsKey(fieldFactory.FieldType))
        {
            return;
        }

        IMongoFieldSerializer? fieldSerializer;

        //build SingleValueSerializer?
        if (fieldFactory.FieldType.GetInterfaces().Any(x => x == typeof(ISingleValueField)))
        {
            //create SingleValueFieldSerializer
            fieldSerializer = (IMongoFieldSerializer?)Activator.CreateInstance(typeof(SingleValueMongoFieldSerializer<>).MakeGenericType(fieldFactory.FieldType));

            if (fieldSerializer == null)
            {
                throw new Exception($"Could not create single value field serializer for '{fieldFactory.FieldType.Name}'.");
            }

            Default.Add(fieldSerializer);
        }
        else //build DefaultFieldSerializer
        {
            fieldSerializer = (IMongoFieldSerializer?)Activator.CreateInstance(typeof(DefaultMongoFieldSerializer<>).MakeGenericType(fieldFactory.FieldType));

            if (fieldSerializer == null)
            {
                throw new Exception($"Could not create default field serializer for '{fieldFactory.FieldType.Name}'.");
            }

            Default.Add(fieldSerializer);
        }
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
