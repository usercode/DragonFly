using DragonFly.AspNetCore.Content;
using DragonFly.Content;
using DragonFly.Core.Builders;
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
public static class PermissionDragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddPermissions(this IDragonFlyBuilder builder)
    {
        builder.Services.Decorate<IContentStorage, ContentStorageAuthorization>();

        builder.Init(api =>
        {
            api.Permission().AddDefaults();
        });

        return builder;
    }
}