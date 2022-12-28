// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;
using DragonFly.Identity.Client.Components.Roles;
using DragonFly.Identity.Client.Components.Users;

namespace DragonFly.Identity.Client;

public class IdentityModule : ClientModule
{
    public override string Name => "Identity";

    public override string Author => "DragonFly";

    public override void Init(IDragonFlyApi api)
    {
        api.Settings().Add<UserList>("Users");
        api.Settings().Add<RoleList>("Roles");
    }
}
