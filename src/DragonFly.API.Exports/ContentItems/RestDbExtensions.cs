// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API.Json;
using System.Text.Json.Nodes;

namespace DragonFly.Models;

public static class RestDbExtensions
{
    public static void ToModelValue(this JsonNode? jsonNode, string fieldName, IContentElement content, ISchemaElement schema)
    {
        if (jsonNode == null)
        {
            return;
        }

        if (schema.Fields.TryGetValue(fieldName, out SchemaField? schemaField))
        {
            IJsonFieldSerializer fieldSerializer = JsonFieldManager.Default.GetByFieldType(ContentFieldManager.Default.GetContentFieldType(schemaField.FieldType));

            ContentField field = fieldSerializer.Read(schemaField, jsonNode);

            content.TrySetField(fieldName, field);
        }
    }

    public static JsonNode? ToRestValue(this ContentField field)
    {
        return ToRestValue(field, true);
    }

    public static JsonNode? ToRestValue(this ContentField field, bool includeNavigationProperties)
    {
        IJsonFieldSerializer fieldSerializer = JsonFieldManager.Default.GetByFieldType(field.GetType());

        return fieldSerializer.Write(field, includeNavigationProperties);
    }
}
