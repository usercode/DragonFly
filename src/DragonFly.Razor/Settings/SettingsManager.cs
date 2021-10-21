using DragonFly.Razor.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Settings
{
    public class SettingsManager
    {
        private static SettingsManager _default;

        public static SettingsManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new SettingsManager();
                }

                return _default;
            }
        }

        public SettingsManager()
        {
            Items = new List<SettingsItem>();
        }

        public IList<SettingsItem> Items { get; }
    }
}
