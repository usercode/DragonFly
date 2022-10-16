// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;
using DragonFly.Razor;
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
