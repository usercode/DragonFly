// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;
using System.Text.Json;

namespace DragonFly.BlockField.Json;

public class NullableEnumToStringConverter<TEnum> : JsonConverter<TEnum?>
{
    private readonly Type _type;

    public NullableEnumToStringConverter()
    {
        _type = Nullable.GetUnderlyingType(typeof(TEnum)) ?? throw new ArgumentNullException();
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(TEnum);
    }

    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();

        if (string.IsNullOrEmpty(value))
        {
            return default;
        }

        if (Enum.TryParse(_type, value, false, out object? result))
        {
            return (TEnum)result;
        }

        return default;
    }

    public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
