// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
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
        var group = endpoints.MapGroup("api/identity");

        group.MapGet("CurrentUser", CurrentUserAsync);
        group.MapUserApi();
        group.MapRoleApi();

        return endpoints;
    }

    private static async Task CurrentUserAsync(HttpContext context, ILoginService service)
    {
        IdentityUser? currentUser = await service.GetCurrentUserAsync();

        await context.Response.WriteAsJsonAsync(currentUser);
    }
}
