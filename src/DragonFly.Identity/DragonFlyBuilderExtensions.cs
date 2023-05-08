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

        builder.AddPermissionScheme(IdentityAuthenticationDefaults.AuthenticationScheme);
        builder.AddPermissions(
                                IdentityPermissions.ManageUser, 
                                IdentityPermissions.QueryUser, 
                                IdentityPermissions.ReadUser, 
                                IdentityPermissions.CreateUser, 
                                IdentityPermissions.UpdateUser, 
                                IdentityPermissions.DeleteUser,
                                IdentityPermissions.ChangePassword,
                                IdentityPermissions.ManageRole,
                                IdentityPermissions.QueryRole,
                                IdentityPermissions.ReadRole,
                                IdentityPermissions.CreateRole,
                                IdentityPermissions.UpdateRole,
                                IdentityPermissions.DeleteRole
                                );

        builder.PostInit<SeedDataAction>();

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapIdentity(this IDragonFlyMiddlewareBuilder builder)
    {
        builder.Endpoints(x => x.MapIdentityApi());

        return builder;
    }
}
