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

    private IDictionary<Type, IJsonFieldSerializer> _fields = new Dictionary<Type, IJsonFieldSerializer>();

    private JsonFieldManager()
    {
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

    /// <summary>
    /// Ensures that the content field has a <see cref="SingleValueJsonFieldSerializer{T}"/>  or <see cref="DefaultJsonFieldSerializer{T}"/>.
    /// </summary>
    public void EnsureField(Type fieldType)
    {
        if (_fields.ContainsKey(fieldType))
        {
            return;
        }

        IJsonFieldSerializer? fieldSerializer;

        if (fieldType.GetInterfaces().Any(x => x == typeof(ISingleValueField)))
        {
            //create SingleValueFieldSerializer
            fieldSerializer = (IJsonFieldSerializer?)Activator.CreateInstance(typeof(SingleValueJsonFieldSerializer<>).MakeGenericType(fieldType));

            if (fieldSerializer == null)
            {
                throw new Exception($"Could not create single value field serializer for '{fieldType.Name}'.");
            }

            Default.Add(fieldSerializer);
        }
        else //build DefaultFieldSerializer
        {
            fieldSerializer = (IJsonFieldSerializer?)Activator.CreateInstance(typeof(DefaultJsonFieldSerializer<>).MakeGenericType(fieldType));

            if (fieldSerializer == null)
            {
                throw new Exception($"Could not create default field serializer for '{fieldType.Name}'.");
            }

            Default.Add(fieldSerializer);
        }        
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
