// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DragonFly.API.Core.Json;

internal class MyConverter : JsonConverter<JsonObject?>
{
    public override JsonObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var node = JsonNode.Parse(ref reader);

        if (node == null)
        {
            return null;
        }

        if (node is JsonObject jsonObject)
        {
            return jsonObject;
        }

        throw new Exception("JSON ERROR");
    }

    public override void Write(Utf8JsonWriter writer, JsonObject? value, JsonSerializerOptions options)
    {
        if (value != null)
        {
            value.WriteTo(writer);
        }
    }
}
