// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity;
using DragonFly.Identity.Commands;
using DragonFly.Identity.Permissions;
using DragonFly.Identity.Rest.Commands;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Identity;

internal static class UserExtensions
{
    public static IEndpointRouteBuilder MapUserApi(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("user");

        group.MapGet("{id:guid}", MapGet).RequirePermission(IdentityPermissions.ReadUser);
        group.MapPost("query", MapQuery).RequirePermission(IdentityPermissions.QueryUser);
        group.MapPost("", MapCreate).RequirePermission(IdentityPermissions.CreateUser);
        group.MapPut("", MapUpdate).RequirePermission(IdentityPermissions.UpdateUser);
        group.MapPost("change-password", MapChangePassword).RequirePermission(IdentityPermissions.ChangePassword);

        return endpoints;
    }

    private static async Task<Results<Ok<IdentityUser>, NotFound>> MapGet(IIdentityService identityService, Guid id)
    {
        IdentityUser user = await identityService.GetUserAsync(id);

        return TypedResults.Ok(user);
    }

    private static async Task<Ok> MapCreate(CreateUser createUser, IIdentityService identityService)
    {
        await identityService.CreateUserAsync(createUser.User, createUser.Password);

        return TypedResults.Ok();
    }

    private static async Task<Ok> MapUpdate(IdentityUser user, IIdentityService identityService)
    {
        await identityService.UpdateUserAsync(user);

        return TypedResults.Ok();
    }

    private static async Task<Ok<IEnumerable<IdentityUser>>> MapQuery(HttpContext context, IIdentityService identityService)
    {
        IEnumerable<IdentityUser> users = await identityService.GetUsersAsync();

        return TypedResults.Ok(users);
    }

    private static async Task<Ok> MapChangePassword(ChangePassword changePassword, IIdentityService identityService)
    {
        await identityService.ChangePasswordAsync(changePassword.UserId, changePassword.NewPassword);

        return TypedResults.Ok();
    }
}
