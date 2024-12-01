// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// MainMenuItem
/// </summary>
public class MenuItem
{
    public MenuItem(string title, Icon icon, string route)
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

    private Icon _icon;

    /// <summary>
    /// CssIcon
    /// </summary>
    public Icon? Icon 
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
