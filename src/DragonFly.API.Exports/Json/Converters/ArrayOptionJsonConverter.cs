// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DragonFly.Core.Json;

public class ArrayOptionJsonConverter : JsonConverter<ContentFieldOptions>
{
    public override ContentFieldOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (JsonDocument.TryParseValue(ref reader, out JsonDocument? doc))
        {
            if (doc.RootElement.TryGetProperty("Type", out JsonElement typeElement))
            {
                string? typeName = typeElement.GetString();

                if (typeName == null)
                {
                    return null;
                }

                Type? contentFieldType = ContentFieldManager.Default.GetOptionsTypeByName(typeName);

                if (contentFieldType == null)
                {
                    return null;
                }

                return (ContentFieldOptions?)JsonSerializer.Deserialize(doc.RootElement, contentFieldType, options);
            }
        }           

        return null;
    }

    public override void Write(Utf8JsonWriter writer, ContentFieldOptions value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
