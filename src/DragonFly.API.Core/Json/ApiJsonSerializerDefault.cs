// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;

namespace DragonFly.API;

public static class ApiJsonSerializerDefault
{
    private static JsonSerializerOptions? _options;

    /// <summary>
    /// Options
    /// </summary>
    public static JsonSerializerOptions Options
    {
        get
        {
            if (_options == null)
            {
                _options = new JsonSerializerOptions();
                _options.TypeInfoResolver = ApiJsonTypeInfoResolver.Default;
            }

            return _options;
        }
    }
}
