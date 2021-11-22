using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DragonFly.Core.Json
{
    public class QueryFieldJsonConverter : JsonConverter<FieldQuery>
    {
        public override FieldQuery? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

                    Type? contentFieldType = ContentFieldManager.Default.GetQueryByName(typeName);

                    if (contentFieldType == null)
                    {
                        return null;
                    }

                    return (FieldQuery?)JsonSerializer.Deserialize(doc.RootElement, contentFieldType, options);
                }
            }           

            return null;
        }

        public override void Write(Utf8JsonWriter writer, FieldQuery value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}