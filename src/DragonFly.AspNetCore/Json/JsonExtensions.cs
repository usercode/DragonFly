// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DragonFly.AspNetCore;

public static class JsonExtensions
{
    public static void AddJsonContext(this JsonSerializerOptions jsonSerializerOptions, IJsonTypeInfoResolver resolver)
    {
        if (jsonSerializerOptions.TypeInfoResolver != null)
        {
            jsonSerializerOptions.TypeInfoResolverChain.Add(resolver);
        }
    }
}
