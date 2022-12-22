// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.AssetFolders;
using DragonFly.AspNetCore.API.Middlewares.Assets;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
using DragonFly.Builders;
using DragonFly.AspNetCore.API.Middlewares.ContentStructures;
using DragonFly.AspNetCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using DragonFly.API.Exports.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using DragonFly.AspNetCore.API.Exports.Json;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddRestApi(this IDragonFlyBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.TypeInfoResolver = JsonSerializerDefault.Options.TypeInfoResolver;
        });

        builder.PostInit<JsonDerivedTypesAction>();

        return builder;
    }

    public static IDragonFlyFullBuilder MapRestApi(this IDragonFlyFullBuilder builder)
    {
        builder.Endpoints(endpoints =>
        {
            RouteGroupBuilder group = endpoints.MapGroup("api");

            group.MapContentItemRestApi();
            group.MapContentSchemaRestApi();
            group.MapContentStructureRestApi();
            group.MapContentNodeRestApi();
            group.MapAssetRestApi();
            group.MapAssetFolderRestApi();
            group.MapWebHookRestApi();
        });

        return builder;
    }
}
