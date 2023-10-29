// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.API;
using DragonFly.AspNetCore.Builders;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddRestApi(this IDragonFlyBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.TypeInfoResolver = JsonSerializerDefault.Options.TypeInfoResolver;
            //opt.SerializerOptions.AddJsonContext<ApiJsonSerializerContext>();
        });

        builder.Init(api => api.JsonFields().AddDefaults());

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapRestApi(this IDragonFlyMiddlewareBuilder builder)
    {
        builder.Endpoints(endpoints =>
        {
            RouteGroupBuilder group = endpoints.MapGroup("api")
                                                .RequirePermission()
                                                .WithDisplayName("DragonFly.API");

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
