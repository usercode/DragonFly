// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.Identity.Middlewares.Roles;
using DragonFly.AspNetCore.Identity.Middlewares.Users;
using DragonFly.Identity;
using DragonFly.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.Identity.Middlewares;

internal static class Extensions
{
    public static IDragonFlyEndpointRouteBuilder MapIdentityApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("identity/CurrentUser", CurrentUserAsync);
        endpoints.MapUserApi();
        endpoints.MapRoleApi();

        return endpoints;
    }

    private static async Task CurrentUserAsync(HttpContext context, ILoginService service)
    {
        IdentityUser? currentUser = await service.GetCurrentUserAsync();

        await context.Response.WriteAsJsonAsync(currentUser);
    }
}
