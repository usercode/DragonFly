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
        Type genericType;

        //non-nullable enum
        if (typeToConvert.IsEnum)
        {
            genericType = typeof(EnumToStringConverter<>).MakeGenericType(typeToConvert);
        }
        //nullable enum
        else if (Nullable.GetUnderlyingType(typeToConvert) is Type underlyingType)
        {
            genericType = typeof(NullableEnumToStringConverter<>).MakeGenericType(underlyingType);
        }
        else
        {
            return null;
        }

        return Activator.CreateInstance(genericType) as JsonConverter;
    }
}
