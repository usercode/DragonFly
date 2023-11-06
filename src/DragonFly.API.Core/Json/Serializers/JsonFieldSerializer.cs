// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;

namespace DragonFly.API;

/// <summary>
/// JsonFieldSerializer
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public abstract class JsonFieldSerializer<TContentField> : IJsonFieldSerializer
    where TContentField : ContentField
{
    public Type FieldType => typeof(TContentField);

    public abstract TContentField Read(SchemaField schemaField, JsonNode? bsonValue);

    public abstract JsonNode? Write(TContentField contentField, bool includeNavigationProperty);

    public JsonNode? Write(ContentField contentField, bool includeNavigationProperty)
    {
        return Write((TContentField)contentField, includeNavigationProperty);
    }

    ContentField IJsonFieldSerializer.Read(SchemaField definition, JsonNode? bsonValue)
    {
        return Read(definition, bsonValue);
    }
}
