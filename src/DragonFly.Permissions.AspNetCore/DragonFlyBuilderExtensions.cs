using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.Content;
using DragonFly.AspNetCore.Middleware;
using DragonFly.Content;
using DragonFly.ContentItems;
using DragonFly.Core.Builders;
using DragonFly.Core.Permissions;
using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore;
using DragonFly.Permissions.AspNetCore.Content;
using DragonFly.Permissions.AspNetCore.Providers;
using DragonFly.Permissions.AspNetCore.Services;
using DragonFly.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        builder.Services.Decorate<IAuthorizePermissionService, DisablePermissionService>();

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