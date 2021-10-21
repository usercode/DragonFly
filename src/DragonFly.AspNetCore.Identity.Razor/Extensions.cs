using DragonFly.AspNetCore.Identity.Razor.Components;
using DragonFly.Core.Builders;
using DragonFly.Razor.Pages.Settings;
using DragonFly.Security;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Razor
{
    public static class Extensions
    {
        public static IDragonFlyBuilder AddIdentity(this IDragonFlyBuilder builder)
        {
            builder.Services.AddTransient<ILoginService, IdentityLoginService>();

            builder.Init(api => api.RegisterModule<Module>());

            return builder;
        }
    }
}
