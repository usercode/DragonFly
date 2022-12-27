// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;
using DragonFly.Models;

namespace DragonFly.API.Json;

/// <summary>
/// ArrayJsonFieldSerializer
/// </summary>
public class ArrayJsonFieldSerializer : JsonFieldSerializer<ArrayField>
{
    public override ArrayField Read(SchemaField schemaField, JsonNode? bsonvalue)
    {
        ArrayField contentField = new ArrayField();

        if (bsonvalue is JsonArray jsonArray)
        {
            if (contentField is ArrayField arrayField)
            {
                ArrayFieldOptions? arrayOptions = schemaField.Options as ArrayFieldOptions;

                if (arrayOptions == null)
                {
                    throw new Exception();
                }

                foreach (JsonNode? item in jsonArray)
                {
                    if (item is JsonObject itemJsonObject)
                    {
                        ArrayFieldItem arrayFieldItem = arrayOptions.CreateArrayField();

                        foreach (var subitem in itemJsonObject)
                        {
                            subitem.Value.ToModelValue(subitem.Key, arrayFieldItem, arrayOptions);
                        }

                        arrayField.Items.Add(arrayFieldItem);
                    }
                }
            }
        }

        return contentField;
    }

    public override JsonNode? Write(ArrayField contentField, bool includeNavigationProperty)
    {
        if (contentField is ArrayField arrayField)
        {
            JsonArray jsonArray = new JsonArray();

            foreach (ArrayFieldItem item in arrayField.Items)
            {
                JsonObject doc = new JsonObject();

                foreach (var f in item.Fields)
                {
                    doc.Add(f.Key, f.Value.ToRestValue());
                }

                jsonArray.Add(doc);
            }

            return jsonArray;
        }
        else
        {
            return null;
        }
    }
}
