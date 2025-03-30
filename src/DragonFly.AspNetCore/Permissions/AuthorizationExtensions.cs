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

        if (DisableAuthorizationState.IsEnabled.Value)
        {
            return Result.Ok();
        }

        AuthorizationResult result = await authorizationService.AuthorizeAsync(principal, permission.ToAuthorizationPolicy()).ConfigureAwait(false);

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
        return new DisableAuthorizationState();
    }
}
