﻿using DragonFly.AspNetCore.SchemaBuilder;
using DragonFly.AspNetCore.SchemaBuilder.Builder;
using DragonFly.Core.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddSchemaBuilder(this IDragonFlyBuilder builder, Action<ContentSchemaBuilderOptions> config)
    {
        builder.Services.Configure(config);
        builder.Services.AddSingleton<IContentSchemaBuilder, ContentSchemaBuilder>();

        builder.PostInit<ContentSchemaPostInitialize>();

        return builder;
    }
}
