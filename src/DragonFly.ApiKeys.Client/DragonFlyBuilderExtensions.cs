﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.DependencyInjection;
using DragonFly.ApiKeys;
using DragonFly.ApiKeys.Razor.Services;
using DragonFly.ApiKeys.Client;
using DragonFly.Client.Builders;

namespace DragonFly.Client;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
    {
        builder.AddRazorRouting();

        builder.Services.AddTransient<IApiKeyService, ApiKeyService>();

        builder.Init<ApiKeyInitializer>();

        return builder;
    }
}
