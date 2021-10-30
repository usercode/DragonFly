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
    class UpdateUserMiddleware
    {
        private readonly RequestDelegate _next;

        public UpdateUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IIdentityService userStore)
        {
            IdentityUser? user = await context.Request.ReadFromJsonAsync<IdentityUser>();

            await userStore.UpdateUserAsync(user);
                     
        }
    }
}
