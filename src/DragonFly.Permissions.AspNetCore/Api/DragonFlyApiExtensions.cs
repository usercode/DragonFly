using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DragonFly.Permissions;

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

        AuthorizationResult result = await permissionService.AuthorizeAsync(httpContextAccessor.HttpContext!.User, permission);

        if (result.Succeeded == false)
        {
            throw new Exception($"Access denied: {permission}");
        }
    }
}
