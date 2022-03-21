using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.MainMenu;

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
