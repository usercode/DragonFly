// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client;

/// <summary>
/// MainMenuItem
/// </summary>
public class MenuItem
{
    public MenuItem(string title, string cssIcon, string route)
    {
        Title = title;
        CssIcon = cssIcon;
        Route = route;
    }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// CssIcon
    /// </summary>
    public string CssIcon { get; set; }

    /// <summary>
    /// Route
    /// </summary>
    public string Route { get; set; }
}
