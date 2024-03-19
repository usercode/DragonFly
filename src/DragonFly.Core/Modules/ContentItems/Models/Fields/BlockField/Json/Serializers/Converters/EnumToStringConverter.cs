// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;
using System.Text.Json;

namespace DragonFly.Json;

public class EnumToStringConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(TEnum);
    }

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();

        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        if (Enum.TryParse(value, false, out TEnum result))
        {
            return result;
        }

        return default;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
