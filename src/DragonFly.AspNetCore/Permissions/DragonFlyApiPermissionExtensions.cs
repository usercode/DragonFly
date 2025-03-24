// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using SmartResults;
using System.Security.Claims;

namespace DragonFly;

public static class DragonFlyApiPermissionExtensions
{
    /// <summary>
    /// Authorizes a permission.
    /// </summary>
    public static async Task<Result> AuthorizeAsync(this IDragonFlyApi api, ClaimsPrincipal? principal, Permission permission)
    {
        IAuthorizationService authorizationService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.AuthorizeAsync(principal, permission).ConfigureAwait(false);
    }
}
