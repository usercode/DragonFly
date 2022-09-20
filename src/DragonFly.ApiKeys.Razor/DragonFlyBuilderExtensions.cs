using DragonFly.ApiKeys.Razor;
using DragonFly.Builders;
using DragonFly.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DragonFLy.ApiKeys;
using DragonFly.ApiKeys.Razor.Services;

namespace DragonFly.ApiKeys.Razor;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
    {
        builder.AddRazorRouting();

        builder.Services.AddTransient<IApiKeyService, ApiKeyService>();

        builder.Init(api => api.Module().Add<Module>());

        return builder;
    }
}
