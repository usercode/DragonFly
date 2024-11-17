// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.AspNetCore.Identity.MongoDB.Services;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.Identity.AspNetCore.Services;
using DragonFly.Identity.Permissions;
using DragonFly.Identity.Services;
using DragonFly.Security;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.AspNetCore.Identity;
using DragonFly.Identity;
using Microsoft.AspNetCore.Authorization;
using DragonFly.AspNetCore.Builders;
using Microsoft.AspNetCore.Routing;
using DragonFly.Permissions;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddMongoDbIdentity(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<ILoginService, LoginService>();
        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddTransient<IPermissionService, PermissionService>();

        builder.Services.AddSingleton<MongoIdentityStore>();
        builder.Services.AddSingleton<IPasswordHashGenerator, PasswordHashGenerator>();
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

        builder.Services.AddAuthentication()
                            .AddCookie(PermissionConstants.AuthenticationScheme, x => x.LoginPath = "/login");

        builder.Init(api =>
        {
            api.Permission()
                            .Add(IdentityPermissions.ManageUser)
                            .Add(IdentityPermissions.QueryUser)
                            .Add(IdentityPermissions.ReadUser)
                            .Add(IdentityPermissions.CreateUser)
                            .Add(IdentityPermissions.UpdateUser)
                            .Add(IdentityPermissions.DeleteUser)
                            .Add(IdentityPermissions.ChangePassword)
                            .Add(IdentityPermissions.ManageRole)
                            .Add(IdentityPermissions.QueryRole)
                            .Add(IdentityPermissions.ReadRole)
                            .Add(IdentityPermissions.CreateRole)
                            .Add(IdentityPermissions.UpdateRole)
                            .Add(IdentityPermissions.DeleteRole);
        });

        builder.PostInit<SeedDataInitializer>();

        builder.AddEndpoint(x => x.MapIdentityApi());

        return builder;
    }
}
