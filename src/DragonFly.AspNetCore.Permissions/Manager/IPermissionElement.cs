using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Permissions
{
    public interface IPermissionElement
    {
        PermissionItem Add(PermissionItem permissionItem);
    }
}
