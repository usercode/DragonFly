using DragonFly.Identity.Razor.Components;
using DragonFly.Identity.Razor.Components.Roles;
using DragonFly.Identity.Razor.Components.Users;
using DragonFly.Razor.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Razor
{
    public class Module : ClientModule
    {
        public override string Name => "Identity";

        public override string Author => "DragonFly";

        public override void Init(IDragonFlyApi api)
        {
            api.Settings().Add<UserList>("Users");
            api.Settings().Add<RoleList>("Roles");
        }
    }
}
