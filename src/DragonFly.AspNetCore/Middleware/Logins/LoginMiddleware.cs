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
using DragonFly.Security;

namespace DragonFly.AspNetCore.API.Middlewares.Logins;

class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ILoginService loginService)
    {
        if (context.Request.Path == "/login" && context.Request.Method == HttpMethods.Post)
        {
            LoginData? loginData = await context.Request.ReadFromJsonAsync<LoginData>();

            if (loginData == null)
            {
                throw new Exception("Login data are not available.");
            }

            if (await loginService.LoginAsync(loginData.Username, loginData.Password, loginData.IsPersistent))
            {
                //await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { }, "password")));
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(3));

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
        else
        {
            await _next(context);
        }
    }
}
