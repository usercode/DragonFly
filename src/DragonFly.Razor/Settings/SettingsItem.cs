using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.Settings
{
    /// <summary>
    /// SettingsItem
    /// </summary>
    public class SettingsItem
    {
        public SettingsItem(string name, Type componentType)
        {
            Name = name;
            ComponentType = componentType;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// ComponentType
        /// </summary>
        public Type ComponentType { get; }

    }
}
