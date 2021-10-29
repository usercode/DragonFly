using DragonFly.Core.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class DragonFlyApiExtensions
    {
        public static PermissionManager Permissions(this IDragonFlyApi api)
        {
            return PermissionManager.Default;
        }


    }
}
