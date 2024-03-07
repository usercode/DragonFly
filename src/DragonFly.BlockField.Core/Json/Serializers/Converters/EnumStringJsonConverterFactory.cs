// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;
using System.Text.Json;

namespace DragonFly.BlockField.Json;

public class EnumStringJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum || (Nullable.GetUnderlyingType(typeToConvert) is Type underlyingType && underlyingType.IsEnum);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type converterType;

        //non-nullable enum
        if (typeToConvert.IsEnum)
        {
            converterType = typeof(EnumToStringConverter<>);
        }
        else //nullable enum
        {
            converterType = typeof(NullableEnumToStringConverter<>);
        }

        Type genericType = converterType.MakeGenericType(typeToConvert);

        return Activator.CreateInstance(genericType) as JsonConverter;
    }
}
