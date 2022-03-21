using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.Identity.Middlewares.Roles;
using DragonFly.AspNetCore.Identity.Middlewares.Users;
using DragonFly.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Middlewares;

internal static class Extensions
{
    public static IDragonFlyEndpointRouteBuilder MapIdentityApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapUserApi();
        endpoints.MapRoleApi();

        return endpoints;
    }
}
