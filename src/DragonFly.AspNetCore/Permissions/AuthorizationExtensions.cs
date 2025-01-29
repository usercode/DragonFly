// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Permissions;
using Microsoft.AspNetCore.Authorization;
using SmartResults;
using System.Security.Claims;

namespace DragonFly;

public static class AuthorizationExtensions
{
    /// <summary>
    /// Authorizes a permission.
    /// </summary>
    public static async Task<Result> AuthorizeAsync(this IAuthorizationService authorizationService, ClaimsPrincipal? principal, Permission permission)
    {
        if (principal == null)
        {
            return Result.Ok();
        }

        AuthorizationResult result = await authorizationService.AuthorizeAsync(principal, permission.ToAuthorizationPolicy());

        if (result.Succeeded)
        {
            return Result.Ok();
        }
        else
        {
            return Result.Failed(new PermissionError(permission));
        }
    }

    public static IDisposable DisableAuthorization(this IDragonFlyApi api)
    {
        var context = api.ServiceProvider.GetRequiredService<IPrincipalContext>();

        return new DisableAuthorization(context);
    }

    public static async Task UseNoAuthorizationAsync(this IDragonFlyApi api, Func<Task> action)
    {
        using (DisableAuthorization(api))
        {
            await action();
        }
    }

    public static async Task<T> UseNoAuthorizationAsync<T>(this IDragonFlyApi api, Func<Task<T>> action)
    {
        using (DisableAuthorization(api))
        {
            return await action();
        }
    }

    public static void UseNoAuthorization(this IDragonFlyApi api, Action action)
    {
        using (DisableAuthorization(api))
        {
            action();
        }
    }

}
