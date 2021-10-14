using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Modules.Base
{
    /// <summary>
    /// ModuleManager
    /// </summary>
    public class ModuleManager
    {
        private static ModuleManager _default;

        public static ModuleManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new ModuleManager();
                }

                return _default;
            }
        }

        public ModuleManager()
        {
            Modules = new List<ClientModule>();
        }

        public IList<ClientModule> Modules { get; }
    }
}
