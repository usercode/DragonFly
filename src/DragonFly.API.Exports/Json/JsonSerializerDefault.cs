// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly.AspNetCore.API.Exports.Json;

public class JsonSerializerDefault
{
    private static JsonSerializerOptions? _options;

    public static JsonSerializerOptions Options
    {
        get
        {
            if (_options == null)
            {
                _options = new JsonSerializerOptions();
                _options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }

            return _options;
        }
    }
}
