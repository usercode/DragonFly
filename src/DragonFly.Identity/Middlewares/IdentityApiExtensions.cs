// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.AspNetCore.Exports;
using DragonFly.Identity;
using DragonFly.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Identity;

internal static class IdentityApiExtensions
{
    public static IEndpointConventionBuilder MapIdentityApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("api/identity");

        group.MapPost("login", MapLogin).AllowAnonymous();
        group.MapGet("CurrentUser", CurrentUserAsync).RequirePermission();

        group.MapUserApi();
        group.MapRoleApi();

        return group;
    }

    private static async Task<Ok<LoginResult>> MapLogin(LoginData loginData, ILoginService loginService)
    {
        LoginResult result = await loginService.LoginAsync(loginData.Username, loginData.Password, loginData.IsPersistent);

        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IdentityUser>, ForbidHttpResult>> CurrentUserAsync(ILoginService service)
    {
        IdentityUser? currentUser = await service.GetCurrentUserAsync();

        if (currentUser != null)
        {
            return TypedResults.Ok(currentUser);
        }
        else
        {
            return TypedResults.Forbid();
        }
    }
}
