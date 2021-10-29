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
    class UpdateRoleMiddleware
    {
        private readonly RequestDelegate _next;

        public UpdateRoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IIdentityService userStore)
        {
            IdentityRole? role = await context.Request.ReadFromJsonAsync<IdentityRole>();

            await userStore.UpdateRoleAsync(role);
                     
        }
    }
}
