// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore;

public static class AuthorizationExtensions
{
    /// <summary>
    /// Requires permission for default schemes.
    /// </summary>
    public static TBuilder RequirePermission<TBuilder>(this TBuilder builder, Permission permission)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(permission.ToAuthorizationPolicy());
    }

    /// <summary>
    /// Requires authorization for default schemes.
    /// </summary>
    public static TBuilder RequirePermission<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(x => x
                                                .RequireAuthenticatedUser()
                                                .AddAuthenticationSchemes(PermissionSchemeManager.GetAll())
                                                );
    }
}
