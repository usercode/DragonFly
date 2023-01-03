// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.Proxy;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddProxy(this IDragonFlyBuilder builder, Action<ContentProxyBuilderOptions> config)
    {
        builder.Services.Configure(config);
        builder.Services.AddSingleton<ContentProxyBuilder>();

        builder.PostInit<ContentProxyPostInitialize>();

        return builder;
    }
}
