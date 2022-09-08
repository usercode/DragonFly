using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Json;

class BlockFieldConverter : JsonConverter<Block>
{
    public override Block? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (JsonDocument.TryParseValue(ref reader, out JsonDocument? doc))
        {
            if (doc.RootElement.TryGetProperty("Type", out JsonElement typeElement))
            {
                string? typeName = typeElement.GetString();

                if (typeName != null && BlockFieldManager.Default.TryGetBlockTypeByName(typeName, out Type? blockType))
                {
                    return (Block?)JsonSerializer.Deserialize(doc.RootElement, blockType, options);
                }

                return new UnknownBlock(doc.RootElement.ToString());
            }
        }

        throw new Exception();
    }

    public override void Write(Utf8JsonWriter writer, Block value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
