using DragonFly.Razor.Pages.Settings.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Modules
{
    /// <summary>
    /// SettingsModule
    /// </summary>
    public class SettingsModule : ClientModule
    {
        public override string Name => "Settings";

        public override string Description => "Manage settings";

        public override string Author => "DragonFly";

        public override void Init(IDragonFlyApi api)
        {
            api.MainMenu().Add("Settings", "fas fa-tools icon", "settings");

            api.Settings().Add<ClientModules>("Modules");
        }
    }
}
