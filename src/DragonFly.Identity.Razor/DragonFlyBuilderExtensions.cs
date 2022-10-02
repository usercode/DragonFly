// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;
using DragonFly.Identity.Razor.Services;
using DragonFly.Identity.Services;
using DragonFly.Permissions;
using DragonFly.Permissions.Client;
using DragonFly.Razor;
using DragonFly.Security;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore.Identity.Razor;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddIdentity(this IDragonFlyBuilder builder)
    {
        builder.AddRazorRouting();

        builder.Services.AddTransient<ILoginService, LoginService>();
        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddTransient<IPermissionService, ClientPermissionService>();

        builder.Init(api => api.Module().Add<Module>());

        return builder;
    }
}
