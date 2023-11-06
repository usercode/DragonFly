// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;

namespace DragonFly.API;

public static class ApiJsonSerializerDefault
{
    /// <summary>
    /// Options
    /// </summary>
    public static JsonSerializerOptions Options { get; } = new JsonSerializerOptions()
                                                                    {
                                                                        TypeInfoResolver = ApiJsonTypeInfoResolver.Default 
                                                                    };
}
