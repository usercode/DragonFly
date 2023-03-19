// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// DragonFlyApiExtensions
/// </summary>
public static class DragonFlyApiExtensions
{
    public static async Task AuthorizeAsync(this IDragonFlyApi api, string permission)
    {
        if (Permission.IsEnabled == false)
        {
            return;
        }

        ClaimsPrincipal? principal = Permission.GetPrincipal();

        if (principal == null)
        {
            return;
        }
        
        IAuthorizationService permissionService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();

        AuthorizationResult result = await permissionService.AuthorizeAsync(principal, permission);

        if (result.Succeeded == false)
        {
            throw new Exception($"Access denied: {permission}");
        }
    }
}
