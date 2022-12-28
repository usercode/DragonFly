// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;

namespace DragonFly.Client;

/// <summary>
/// MenuItemManager
/// </summary>
public class MenuItemManager
{
    private static MenuItemManager _default;

    public static MenuItemManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new MenuItemManager();
            }

            return _default;
        }
    }

    public MenuItemManager()
    {
        Items = new List<MenuItem>();
    }

    public IList<MenuItem> Items { get; private set; }

    public void Add(string title, string cssIcon, string route)
    {
        Items.Add(new MenuItem(title, cssIcon, route));
    }
}
