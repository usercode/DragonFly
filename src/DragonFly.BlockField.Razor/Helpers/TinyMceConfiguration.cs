// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField.Razor.Helpers;

public class TinyMceConfiguration
{
    public static Dictionary<string, object> Editor = new Dictionary<string, object>
    {
        { "menubar", false },
        { "plugins", "link image code table lists autoresize" },
        { "toolbar", "bold italic forecolor alignleft aligncenter alignright bullist numlist removeformat code" }
    };
}
