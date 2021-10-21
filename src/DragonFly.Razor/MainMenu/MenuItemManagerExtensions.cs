using DragonFly.Razor;
using DragonFly.Razor.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class MenuItemManagerExtensions
    {
        public static MenuItemManager MainMenu(this IDragonFlyApi api)
        {
            return MenuItemManager.Default;
        }
    }
}
