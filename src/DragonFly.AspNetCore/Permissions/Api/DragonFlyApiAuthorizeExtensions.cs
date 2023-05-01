// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace DragonFly;

/// <summary>
/// DragonFlyApiAuthorizeExtensions
/// </summary>
public static class DragonFlyApiAuthorizeExtensions
{
    public static async Task AuthorizeAsync(this IDragonFlyApi api, string permission)
    {
        ClaimsPrincipal? principal = Permission.GetCurrentPrincipal();

        if (principal == null)
        {
            return;
        }
        
        IAuthorizationService authorizationService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();

        AuthorizationResult result = await authorizationService.AuthorizeAsync(principal, permission);

        if (result.Succeeded == false)
        {
            throw new Exception($"Access denied: {permission}");
        }
    }
}
