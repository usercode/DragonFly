using DragonFly.Razor.MainMenu;
using DragonFly.Razor.Pages.Settings;
using DragonFly.Razor.Settings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public static class SettingsManagerExtensions
{
    public static SettingsManager Settings(this IDragonFlyApi api)
    {
        return SettingsManager.Default;
    }       
}
