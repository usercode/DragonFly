// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity.Client.Components.Roles;
using DragonFly.Identity.Client.Components.Users;
using DragonFly.Init;

namespace DragonFly.Identity.Client;

public class IdentityInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {        
        api.Settings.Add<UserList>("Users");
        api.Settings.Add<RoleList>("Roles");

        return Task.CompletedTask;
    }
}
