using DragonFly.Builders;
using DragonFly.Proxy;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddProxies(this IDragonFlyBuilder builder, Action<ContentSchemaBuilderOptions> config)
    {
        builder.Services.Configure(config);
        builder.Services.AddSingleton<IContentSchemaBuilder, ContentSchemaBuilder>();

        builder.PostInit<ContentSchemaPostInitialize>();

        return builder;
    }
}
