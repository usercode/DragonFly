using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Middlewares
{
    class QueryRoleMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryRoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IIdentityService userStore)
        {
            IEnumerable<IdentityRole> users = await userStore.GetRolesAsync();

            await context.Response.WriteAsJsonAsync(users);
        }
    }
}
