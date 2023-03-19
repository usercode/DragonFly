// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Exports;
using Microsoft.AspNetCore.Http;
using DragonFly.Security;

namespace DragonFly.AspNetCore.API.Middlewares.Logins;

class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILoginService loginService)
    {
        if (context.Request.Path == "/login" && context.Request.Method == HttpMethods.Post)
        {
            LoginData? loginData = await context.Request.ReadFromJsonAsync<LoginData>();

            if (loginData == null)
            {
                throw new Exception("Login data are not available.");
            }

            bool valid = await loginService.LoginAsync(loginData.Username, loginData.Password, loginData.IsPersistent);

            if (valid == false)
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
