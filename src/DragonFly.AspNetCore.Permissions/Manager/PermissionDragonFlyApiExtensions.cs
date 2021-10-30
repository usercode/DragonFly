using DragonFly.Core.ContentItems;
using DragonFly.Core.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class PermissionDragonFlyApiExtensions
    {
        public static PermissionManager Permission(this IDragonFlyApi api)
        {
            return PermissionManager.Default;
        }

        public static PermissionManager AddDefaults(this PermissionManager manager)
        {
            manager
                        .Add("Admin", x => x
                                    .Add("Content", x => x
                                                .Add(ContentItemPermissions.ContentRead)
                                                .Add(ContentItemPermissions.ContentCreate)
                                                .Add(ContentItemPermissions.ContentUpdate)
                                                .Add(ContentItemPermissions.ContentDelete)
                                                ));

            return manager;
        }

        public static IPermissionElement Admin(this PermissionManager manager, Action<IPermissionElement>? childs = null)
        {
            return Add(manager, "Admin", childs);
        }

        public static IPermissionElement Add(this IPermissionElement manager, string name, Action<IPermissionElement>? childs = null)
        {
            PermissionItem p = new PermissionItem(name);

            childs?.Invoke(p);

            return manager.Add(p);
        }

       
    }
}