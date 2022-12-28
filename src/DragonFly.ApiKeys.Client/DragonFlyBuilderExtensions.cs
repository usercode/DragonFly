// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Builders;
using Microsoft.Extensions.DependencyInjection;
using DragonFLy.ApiKeys;
using DragonFly.ApiKeys.Razor.Services;
using DragonFly.ApiKeys.Client;

namespace DragonFly.Client;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
    {
        builder.AddRazorRouting();

        builder.Services.AddTransient<IApiKeyService, ApiKeyService>();

        builder.Init(api => api.Module().Add<ApiKeyModule>());

        return builder;
    }
}
