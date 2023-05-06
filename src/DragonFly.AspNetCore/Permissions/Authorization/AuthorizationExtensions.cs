// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Builder;

namespace DragonFly.AspNetCore.Authorization;

public static class AuthorizationExtensions
{
    /// <summary>
    /// RequirePermission
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
}
