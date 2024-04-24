// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using Microsoft.AspNetCore.Authorization;
using Results;
using System.Security.Claims;

namespace DragonFly;

public static class DragonFlyApiPermissionExtensions
{
    /// <summary>
    /// Authorizes access to a content item.
    /// </summary>
    public static async Task<Result> AuthorizeContentAsync(this IDragonFlyApi api, string schema, ContentAction action)
    {
        Permission permission = ContentPermissions.Create(schema, action);

        return await api.AuthorizeAsync(permission);
    }

    /// <summary>
    /// Authorizes a permission.
    /// </summary>
    public static async Task<Result> AuthorizeAsync(this IDragonFlyApi api, Permission permission)
    {
        IPrincipalContext principalContext = Principal(api);

        ClaimsPrincipal? principal = principalContext.Current;

        if (principal == null)
        {
            return Result.Ok();
        }

        IAuthorizationService authorizationService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();

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

    /// <summary>
    /// Gets the principal context.
    /// </summary>
    public static IPrincipalContext Principal(this IDragonFlyApi api)
    {
        return api.ServiceProvider.GetRequiredService<IPrincipalContext>();
    }
}
