using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.Content;
using DragonFly.Content;
using DragonFly.ContentItems;
using DragonFly.Core.Builders;
using DragonFly.Core.Permissions;
using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore;
using DragonFly.Permissions.AspNetCore.Content;
using DragonFly.Permissions.AspNetCore.Services;
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
        builder.Services.Decorate<IPermissionService, DisablePermissionService>();

        builder.Init(api =>
        {
            api.Permission().AddDefaults();
        });

        return builder;
    }

    public static IDragonFlyApplicationBuilder UsePermission(this IDragonFlyApplicationBuilder builder)
    {
        builder
            .UsePermissionItemApi();

        return builder;
    }
}