using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Permissions
{
    public class PermissionItem : IPermissionElement
    {
        public PermissionItem(string name)
        {
            Name = name;
            Childs = new List<PermissionItem>();
        }

        public string Name { get; set; }

        public IList<PermissionItem> Childs { get; }

        public PermissionItem Add(PermissionItem permissionItem)
        {
            PermissionItem? found = Childs.FirstOrDefault(x => x.Name == permissionItem.Name);

            if (found != null)
            {
                return found;
            }

            Childs.Add(permissionItem);

            return permissionItem;
        }
    }
}
