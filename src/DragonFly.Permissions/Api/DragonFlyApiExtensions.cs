// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// DragonFlyApiExtensions
/// </summary>
public static class DragonFlyApiExtensions
{
    public static async Task AuthorizeAsync(this IDragonFlyApi api, string permission)
    {
        if (PermissionState.IsEnabled == false)
        {
            return;
        }

        IHttpContextAccessor httpContextAccessor = api.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        IAuthorizationService permissionService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();

        //temporary fix for background tasks
        if (httpContextAccessor.HttpContext == null)
        {
            return;
        }

        AuthorizationResult result = await permissionService.AuthorizeAsync(httpContextAccessor.HttpContext.User, permission);

        if (result.Succeeded == false)
        {
            throw new Exception($"Access denied: {permission}");
        }
    }
}
