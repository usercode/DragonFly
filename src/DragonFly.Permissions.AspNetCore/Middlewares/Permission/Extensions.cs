using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore;

internal static class Extensions
{
    public static void MapPermissionItemApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("permission/query", MapQuery);
    }

    private static async Task MapQuery(HttpContext context, IDragonFlyApi api)
    {
        IEnumerable<Permission> items = api.Permission().Items;

        await context.Response.WriteAsJsonAsync(items);
    }
}
