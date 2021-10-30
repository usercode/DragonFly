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
    class GetUserMiddleware
    {
        private readonly RequestDelegate _next;

        public GetUserMiddleware(RequestDelegate next)
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

                IdentityUser user = await userStore.GetUserAsync(id);

                await context.Response.WriteAsJsonAsync(user);
            }           
        }
    }
}
