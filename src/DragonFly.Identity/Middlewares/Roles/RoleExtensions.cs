// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Authorization;
using DragonFly.Identity;
using DragonFly.Identity.Permissions;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Identity;

internal static class RoleExtensions
{
    public static IEndpointRouteBuilder MapRoleApi(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("role");

        group.MapGet("{id:guid}", MapGet).RequirePermission(IdentityPermissions.ReadRole);
        group.MapPost("", MapCreate).RequirePermission(IdentityPermissions.CreateRole);
        group.MapPut("", MapUpdate).RequirePermission(IdentityPermissions.UpdateRole);
        group.MapPost("query", MapQuery).RequirePermission(IdentityPermissions.QueryRole);

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
