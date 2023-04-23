// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
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

        builder.Init(api => api.JsonFields().AddDefaults());

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
            group.MapBackgroundTaskApi();
            group.MapPermissionItemApi();
        });

        return builder;
    }
}
