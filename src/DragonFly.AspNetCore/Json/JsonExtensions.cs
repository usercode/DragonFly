// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DragonFly.AspNetCore;

public static class JsonExtensions
{
    public static void AddJsonContext<T>(this JsonSerializerOptions jsonSerializerOptions)
        where T : IJsonTypeInfoResolver, new()
    {
        if (jsonSerializerOptions.TypeInfoResolver != null)
        {
            jsonSerializerOptions.TypeInfoResolver = JsonTypeInfoResolver.Combine(jsonSerializerOptions.TypeInfoResolver, new T());
        }
        else
        {
            jsonSerializerOptions.TypeInfoResolver = new T();
        }
    }
}
