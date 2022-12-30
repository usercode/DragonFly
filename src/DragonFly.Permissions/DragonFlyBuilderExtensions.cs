// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Middleware;
using DragonFly.AspNetCore.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Decorator;
using DragonFly.Permissions;

namespace DragonFly.AspNetCore;

/// <summary>
/// PermissionDragonFlyBuilderExtensions
/// </summary>
public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds a permission layer to storage services.
    /// <br /><br />
    /// Decorated services:
    /// <br />
    /// <see cref="ISchemaStorage"/> <see cref="IContentStorage"/>, <see cref="IAssetStorage"/>
    /// </summary>
    public static IDragonFlyBuilder AddPermissions(this IDragonFlyBuilder builder)
    {
        builder.Services.Decorate<IContentStorage, ContentStorageAuthorization>();
        builder.Services.Decorate<IAssetStorage, AssetStorageAuthorization>();
        builder.Services.Decorate<ISchemaStorage, SchemaStorageAuthorization>();

        builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        builder.Init(api =>
        {
            api.Permission().AddDefaults();
        });

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapPermission(this IDragonFlyMiddlewareBuilder builder)
    {
        builder.Endpoints(endpoints => endpoints.MapPermissionItemApi());

        return builder;
    }
}
