// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore;

/// <summary>
/// PermissionSchemeManager
/// </summary>
public static class PermissionSchemeManager
{
    private static HashSet<string> _items = new HashSet<string>();

    private static string[] _cache = Array.Empty<string>();

    /// <summary>
    /// Adds scheme.
    /// </summary>
    /// <param name="scheme"></param>
    internal static void Add(string scheme)
    {
        _items.Add(scheme);

        _cache = _items.ToArray();
    }

    /// <summary>
    /// Gets available schemes.
    /// </summary>
    /// <returns></returns>
    public static string[] GetAll()
    {
        return _cache;
    }
}
