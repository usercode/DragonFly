// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderPermissionExtensions
{
    /// <summary>
    /// Registers permissions.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="permissions"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddPermissions(this IDragonFlyBuilder builder, params Permission[] permissions)
    {
        foreach (Permission permission in permissions)
        {
            PermissionManager.Default.Add(permission);
        }

        builder.Services.AddAuthorization(x =>
        {
            foreach (Permission permission in permissions)
            {
                x.AddPolicy(permission.Name, x => x
                                                .RequireAuthenticatedUser()
                                                .AddAuthenticationSchemes(PermissionSchemeManager.GetAll())                                                
                                                .AddRequirements(new PermissionRequirement(permission.Name)));
            }
        });

        return builder;
    }

    /// <summary>
    /// Adds permission scheme.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="scheme"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddPermissionScheme(this IDragonFlyBuilder builder, string scheme)
    {
        PermissionSchemeManager.Add(scheme);

        return builder;
    }
}
