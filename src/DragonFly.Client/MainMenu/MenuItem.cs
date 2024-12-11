// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;

namespace DragonFly.Client;

/// <summary>
/// MainMenuItem
/// </summary>
public class MenuItem
{
    public MenuItem(string title, string icon, string route)
    {
        Title = title;
        Icon = icon;
        Route = route;
    }

    private string _title;

    /// <summary>
    /// Title
    /// </summary>
    public string Title 
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value;

                Changed?.Invoke();
            }
        }
    }

    private string _icon;

    /// <summary>
    /// CssIcon
    /// </summary>
    public string? Icon 
    {
        get => _icon;
        set
        {
            if (_icon != value)
            {
                _icon = value;

                Changed?.Invoke();
            }
        }
    }

    /// <summary>
    /// Route
    /// </summary>
    public string Route { get; set; }

    /// <summary>
    /// Changed
    /// </summary>
    public event Action Changed;
}
