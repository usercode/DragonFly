// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Results;
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
}
