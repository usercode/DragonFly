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
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Identity;

internal static class UserExtensions
{
    public static IEndpointRouteBuilder MapUserApi(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("user");

        group.MapGet("{id:guid}", MapGet).RequireAuthorization(IdentityPermissions.UserRead);
        group.MapPost("query", MapQuery).RequireAuthorization(IdentityPermissions.UserQuery);
        group.MapPost("", MapCreate).RequireAuthorization(IdentityPermissions.UserCreate);
        group.MapPut("", MapUpdate).RequireAuthorization(IdentityPermissions.UserUpdate);
        group.MapPost("change-password", MapChangePassword).RequireAuthorization(IdentityPermissions.PasswordChange);

        return endpoints;
    }

    private static async Task MapGet(HttpContext context, IIdentityService identityService, Guid id)
    {
        IdentityUser user = await identityService.GetUserAsync(id);

        await context.Response.WriteAsJsonAsync(user);
    }

    private static async Task MapCreate(HttpContext context, IIdentityService identityService)
    {
        CreateUser? createUser = await context.Request.ReadFromJsonAsync<CreateUser>();

        if (createUser == null)
        {
            throw new Exception();
        }

        await identityService.CreateUserAsync(createUser.User, createUser.Password);
    }

    private static async Task MapUpdate(HttpContext context, IIdentityService identityService)
    {
        IdentityUser? user = await context.Request.ReadFromJsonAsync<IdentityUser>();

        await identityService.UpdateUserAsync(user);
    }

    private static async Task MapQuery(HttpContext context, IIdentityService identityService)
    {
        IEnumerable<IdentityUser> users = await identityService.GetUsersAsync();

        await context.Response.WriteAsJsonAsync(users);
    }

    private static async Task MapChangePassword(HttpContext context, IIdentityService identityService)
    {
        ChangePassword? changePassword = await context.Request.ReadFromJsonAsync<ChangePassword>();

        if (changePassword == null)
        {
            throw new ArgumentException(nameof(changePassword));
        }

        await identityService.ChangePasswordAsync(changePassword.UserId, changePassword.NewPassword);
    }
}
