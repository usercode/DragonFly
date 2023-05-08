// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.AspNetCore.Exports;
using DragonFly.Identity;
using DragonFly.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Identity;

internal static class Extensions
{
    public static IDragonFlyEndpointBuilder MapIdentityApi(this IDragonFlyEndpointBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("api/identity");

        group.MapPost("login", MapLogin).AllowAnonymous();
        group.MapGet("CurrentUser", CurrentUserAsync).RequirePermission();

        group.MapUserApi();
        group.MapRoleApi();

        return endpoints;
    }

    private static async Task<IResult> MapLogin(LoginData loginData, ILoginService loginService)
    {
        bool valid = await loginService.LoginAsync(loginData.Username, loginData.Password, loginData.IsPersistent);

        if (valid)
        {
            return Results.Ok();
        }
        else
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            return Results.StatusCode(StatusCodes.Status401Unauthorized);
        }
    }

    private static async Task CurrentUserAsync(HttpContext context, ILoginService service)
    {
        IdentityUser? currentUser = await service.GetCurrentUserAsync();

        await context.Response.WriteAsJsonAsync(currentUser);
    }
}
