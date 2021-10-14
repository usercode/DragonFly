using DragonFly.Razor;
using DragonFly.Razor.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class DragonFlyApiMenuItemManagerExtensions
    {
        public static MenuItemManager MainMenu(this IDragonFlyApi api)
        {
            return MenuItemManager.Default;
        }

        public static void AddMenuItem(this IDragonFlyApi api, string title, string cssIcon, string route)
        {
            api.MainMenu().Items.Add(new MenuItem(title, cssIcon, route));
        }
    }
}
