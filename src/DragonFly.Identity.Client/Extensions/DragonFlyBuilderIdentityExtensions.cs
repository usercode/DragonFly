// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.Razor;
using DragonFly.Client.Builders;
using DragonFly.Identity;
using DragonFly.Identity.Client;
using DragonFly.Identity.Razor.Services;
using DragonFly.Identity.Services;
using DragonFly.Permissions;
using DragonFly.Permissions.Client;
using DragonFly.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DragonFly.Client;

public static class DragonFlyBuilderIdentityExtensions
{
    public static IDragonFlyBuilder AddIdentity(this IDragonFlyBuilder builder)
    {
        builder.AddRazorRouting();

        builder.Services.TryAddTransient<ILoginService, LoginService>();
        builder.Services.TryAddTransient<IIdentityService, IdentityService>();
        builder.Services.TryAddTransient<IPermissionService, ClientPermissionService>();

        builder.Init<IdentityInitializer>();

        return builder;
    }
}
