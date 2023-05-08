// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace DragonFly;

public static class DragonFlyApiPermissionExtensions
{
    /// <summary>
    /// Gets the permission manager.
    /// </summary>
    /// <param name="api"></param>
    /// <returns></returns>
    public static PermissionManager Permissions(this IDragonFlyApi api)
    {
        return PermissionManager.Default;
    }

    /// <summary>
    /// Authorizes a permission.
    /// </summary>
    /// <param name="api"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static async Task<bool> AuthorizeAsync(this IDragonFlyApi api, Permission permission)
    {
        ClaimsPrincipal? principal = PermissionPrincipal.GetCurrent();

        if (principal == null)
        {
            return false;
        }

        IAuthorizationService authorizationService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();

        AuthorizationResult result = await authorizationService.AuthorizeAsync(principal, permission.Name);

        return result.Succeeded;
    }
}
