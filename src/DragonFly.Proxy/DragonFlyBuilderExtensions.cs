// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;
using DragonFly.Proxy;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddProxy(this IDragonFlyBuilder builder, Action<ContentSchemaBuilderOptions> config)
    {
        builder.Services.Configure(config);
        builder.Services.AddSingleton<IContentSchemaBuilder, ContentSchemaBuilder>();

        builder.PostInit<ContentSchemaPostInitialize>();

        return builder;
    }
}
