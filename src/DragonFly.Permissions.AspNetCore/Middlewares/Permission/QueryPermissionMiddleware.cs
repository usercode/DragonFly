using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore
{
    class QueryPermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryPermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IDragonFlyApi api)
        {
            IEnumerable<Permission> items = api.Permission().Items;

            await context.Response.WriteAsJsonAsync(items);
        }
    }
}
