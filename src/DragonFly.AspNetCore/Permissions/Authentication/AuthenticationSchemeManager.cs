// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore;

public static class AuthenticationSchemeManager
{
    private static IDictionary<string, string> _items = new Dictionary<string, string>();

    public static void Add(string scheme)
    {
        _items[scheme] = scheme;
    }

    public static string[] GetAll()
    {
        return _items.Keys.ToArray();
    }
}
