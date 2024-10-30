// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly.Core;

internal class ContentReferenceConverter : JsonConverter<ContentReference>
{
    public override ContentReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();

        if (value == null)
        {
            throw new Exception();
        }

        int pos = value.IndexOf('/');

        return new ContentReference(value[..pos], Guid.Parse(value[(pos + 1)..]));
    }

    public override void Write(Utf8JsonWriter writer, ContentReference value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"{value.Schema}/{value.Id}");
    }
}
