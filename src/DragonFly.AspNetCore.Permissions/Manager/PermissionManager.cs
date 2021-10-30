using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Permissions
{
    /// <summary>
    /// PermissionManager
    /// </summary>
    public class PermissionManager : IPermissionElement
    {
        private static PermissionManager? _default;

        public static PermissionManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new PermissionManager();
                }

                return _default;
            }
        }

        public PermissionManager()
        {
            Items = new List<PermissionItem>();
        }

        /// <summary>
        /// Items
        /// </summary>
        public IList<PermissionItem> Items { get; }

        public IEnumerable<PermissionItem> Childs => Items;

        public PermissionItem Add(PermissionItem permissionItem)
        {
            PermissionItem? found = Items.FirstOrDefault(x => x.Name == permissionItem.Name);

            if (found != null)
            {
                return found;
            }

            Items.Add(permissionItem);

            return permissionItem;
        }
    }
}
