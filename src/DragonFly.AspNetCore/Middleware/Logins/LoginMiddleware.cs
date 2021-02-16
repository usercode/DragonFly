using DragonFly.AspNetCore.Exports;
using DragonFly.AspNet.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.Logins
{
    class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IOptions<DragonFlyOptions> options)
        {
            if(context.Request.Path == "/login" && context.Request.Method == HttpMethods.Post)
            {
                LoginData loginData = await context.Request.ReadFromJsonAsync<LoginData>();

                if (loginData.Password == options.Value.Password)
                {
                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { }, "password")));
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
            else
            {
                await _next(context);
            }         
        }
    }
}
