using DragonFly.Core.Builders;
using DragonFly.Identity.Razor.Services;
using DragonFly.Identity.Services;
using DragonFly.Permissions.Client;
using DragonFly.Permissions.Services;
using DragonFly.Razor;
using DragonFly.Razor.Pages.Settings;
using DragonFly.Security;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
