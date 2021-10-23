using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Middlewares
{
    class QueryUserMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IUserStore userStore)
        {
            var users = await userStore.GetUsersAsync();

            await context.Response.WriteAsJsonAsync(users);
        }
    }
}
