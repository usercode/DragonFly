// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Builder;

namespace DragonFly.AspNetCore;

public static class AuthorizationExtensions
{
    /// <summary>
    /// Requires permission for default schemes.
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    /// <param name="builder"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static TBuilder RequirePermission<TBuilder>(this TBuilder builder, Permission permission)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(permission.Name);
    }

    /// <summary>
    /// Requires authorization for default schemes.
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static TBuilder RequirePermission<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(x => x
                                                .RequireAuthenticatedUser()
                                                .AddAuthenticationSchemes(PermissionSchemeManager.GetAll())                                                
                                                );
    }
}
