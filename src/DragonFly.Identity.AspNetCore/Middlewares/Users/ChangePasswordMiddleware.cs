using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity.Commands;
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
    class ChangePasswordMiddleware
    {
        private readonly RequestDelegate _next;

        public ChangePasswordMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IIdentityService userStore)
        {
            ChangePassword? changePassword = await context.Request.ReadFromJsonAsync<ChangePassword>();

            if (changePassword == null)
            {
                throw new ArgumentException(nameof(changePassword));
            }

            await userStore.ChangePasswordAsync(changePassword.UserId, changePassword.NewPassword);
        }
    }
}
