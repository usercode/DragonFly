using DragonFly.AspNetCore.Content;
using DragonFly.AspNetCore.Middleware;
using DragonFly.Content;
using DragonFly.Builders;
using DragonFly.Permissions.AspNetCore;
using DragonFly.Permissions.AspNetCore.Content;
using DragonFly.Permissions.AspNetCore.Providers;
using DragonFly.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

/// <summary>
/// PermissionDragonFlyBuilderExtensions
/// </summary>
public static class DragonFlyBuilderExtensions
{
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

    public static IDragonFlyFullBuilder MapPermission(this IDragonFlyFullBuilder builder)
    {
        builder.Endpoints(endpoints => endpoints.MapPermissionItemApi());

        return builder;
    }
}
