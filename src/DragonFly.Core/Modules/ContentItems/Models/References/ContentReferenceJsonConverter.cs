// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly.Core;

internal class ContentReferenceJsonConverter : JsonConverter<ContentReference>
{
    public override ContentReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();

        if (value == null)
        {
            throw new Exception();
        }

        return ContentReference.Parse(value, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, ContentReference value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"{value.Schema}/{value.Id}");
    }
}
