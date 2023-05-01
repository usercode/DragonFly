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

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddMongoDbIdentity(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<ILoginService, LoginService>();
        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.AddSingleton<MongoIdentityStore>();
        builder.Services.AddSingleton<IPasswordHashGenerator, PasswordHashGenerator>();
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        builder.Authentication.AddCookie(IdentityAuthenticationDefaults.AuthenticationScheme);

        AuthenticationSchemeManager.Add(IdentityAuthenticationDefaults.AuthenticationScheme);

        builder.Init(api =>
        {
            api.Permissions()
                            .Add("User", x => x
                                            .Add(IdentityPermissions.UserRead, description: "Read user", sortkey: 0, childs: x => x
                                                    .Add(IdentityPermissions.UserQuery, description: "Query user"))
                                            .Add(IdentityPermissions.UserCreate, description: "Create user", sortkey: 1)
                                            .Add(IdentityPermissions.UserUpdate, description: "Update user", sortkey: 2)
                                            .Add(IdentityPermissions.UserDelete, description: "Delete user", sortkey: 3)
                                            .Add(IdentityPermissions.PasswordChange, description: "Change password", sortkey: 4)
                                            )
                            .Add("Role", x => x
                                            .Add(IdentityPermissions.RoleRead, description: "Read role", sortkey: 0, childs: x => x
                                                    .Add(IdentityPermissions.RoleQuery, description: "Query role"))
                                            .Add(IdentityPermissions.RoleCreate, description: "Create role", sortkey: 1)
                                            .Add(IdentityPermissions.RoleUpdate, description: "Update role", sortkey: 2)
                                            .Add(IdentityPermissions.RoleDelete, description: "Delete role", sortkey: 3)
                            );
        });

        builder.PostInit<SeedDataAction>();

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapIdentity(this IDragonFlyMiddlewareBuilder builder)
    {
        builder.Endpoints(x => x.MapIdentityApi());

        return builder;
    }
}
