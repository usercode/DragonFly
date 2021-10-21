using DragonFly.Razor.MainMenu;
using DragonFly.Razor.Pages.Settings;
using DragonFly.Razor.Settings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class SettingsManagerExtensions
    {
        public static SettingsManager Settings(this IDragonFlyApi api)
        {
            return SettingsManager.Default;
        }

        public static void AddSettings<T>(this IDragonFlyApi api, string title)
            where T : ComponentBase
        {
            AddSettings(api, title, typeof(T));
        }

        public static void AddSettings(this IDragonFlyApi api, string title, Type componentType)
        {
            api.Settings().Items.Add(new SettingsItem(title, componentType));
        }
    }
}
