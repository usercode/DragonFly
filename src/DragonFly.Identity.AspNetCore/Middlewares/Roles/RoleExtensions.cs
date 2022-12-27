// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;
using DragonFly.Identity;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Identity.Middlewares.Roles;

internal static class RoleExtensions
{
    public static IDragonFlyEndpointBuilder MapRoleApi(this IDragonFlyEndpointBuilder endpoints)
    {
        endpoints.MapGet("identity/role/{id:guid}", MapGet);
        endpoints.MapPost("identity/role", MapCreate);
        endpoints.MapPut("identity/role", MapUpdate);            
        endpoints.MapPost("identity/role/query", MapQuery);

        return endpoints;
    }

    private static async Task MapGet(HttpContext context, IIdentityService identityService, Guid id)
    {
        IdentityRole role = await identityService.GetRoleAsync(id);

        await context.Response.WriteAsJsonAsync(role);
    }

    private static async Task MapCreate(HttpContext context, IIdentityService identityService)
    {
        IdentityRole? role = await context.Request.ReadFromJsonAsync<IdentityRole>();

        await identityService.CreateRoleAsync(role);
    }

    private static async Task MapUpdate(HttpContext context, IIdentityService identityService)
    {
        IdentityRole? role = await context.Request.ReadFromJsonAsync<IdentityRole>();

        await identityService.UpdateRoleAsync(role);
    }

    private static async Task MapQuery(HttpContext context, IIdentityService identityService)
    {
        IEnumerable<IdentityRole> users = await identityService.GetRolesAsync();

        await context.Response.WriteAsJsonAsync(users);
    }
}
