// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.AssetFolders;
using DragonFly.AspNetCore.API.Middlewares.Assets;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
using DragonFly.AspNetCore.Builders;
using DragonFly.AspNetCore.API.Middlewares.ContentStructures;
using DragonFly.AspNetCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.API;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddRestApi(this IDragonFlyBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.TypeInfoResolver = JsonSerializerDefault.Options.TypeInfoResolver;
        });

        builder.Init(api => api.JsonField().AddDefaults());
        builder.PostInit<CreateMissingJsonFieldSerializer>();

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapRestApi(this IDragonFlyMiddlewareBuilder builder)
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
