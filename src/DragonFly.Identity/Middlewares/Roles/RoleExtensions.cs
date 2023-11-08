// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity;
using DragonFly.Identity.Permissions;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

    private static async Task<Ok<IdentityRole>> MapGet(IIdentityService identityService, Guid id)
    {
        IdentityRole role = await identityService.GetRoleAsync(id);

        return TypedResults.Ok(role);
    }

    private static async Task<Ok> MapCreate(IdentityRole role, IIdentityService identityService)
    {
        await identityService.CreateRoleAsync(role);

        return TypedResults.Ok();
    }

    private static async Task<Ok> MapUpdate(IdentityRole role, IIdentityService identityService)
    {
        await identityService.UpdateRoleAsync(role);

        return TypedResults.Ok();
    }

    private static async Task<Ok<IEnumerable<IdentityRole>>> MapQuery(HttpContext context, IIdentityService identityService)
    {
        IEnumerable<IdentityRole> users = await identityService.GetRolesAsync();

        return TypedResults.Ok(users);
    }
}
