using DragonFly.Razor.Modules;
using DragonFly.Razor.Modules.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class DragonFlyApiModuleManagerExtensions
    {
        public static ModuleManager Module(this IDragonFlyApi api)
        {
            return ModuleManager.Default;
        }

        public static void RegisterModule<TModule>(this IDragonFlyApi api)
            where TModule : ClientModule, new()
        {
            api.Module().Modules.Add(new TModule());
        }
    }
}
