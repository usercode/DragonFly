using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Middlewares
{
    class GetRoleMiddleware
    {
        private readonly RequestDelegate _next;

        public GetRoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IIdentityService userStore)
        {
            if (context.GetRouteValue("id") is string stringId)
            {
                Guid id = Guid.Parse(stringId);

                IdentityRole role = await userStore.GetRoleAsync(id);

                await context.Response.WriteAsJsonAsync(role);
            }
        }
    }
}
