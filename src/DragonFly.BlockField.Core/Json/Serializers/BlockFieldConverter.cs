// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly.BlockField;

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

                return new UnknownBlock(doc.RootElement);
            }
        }

        throw new Exception();
    }

    public override void Write(Utf8JsonWriter writer, Block value, JsonSerializerOptions options)
    {
        if (value is UnknownBlock unknownBlock)
        {
            unknownBlock.Node.WriteTo(writer);
        }
        else
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
