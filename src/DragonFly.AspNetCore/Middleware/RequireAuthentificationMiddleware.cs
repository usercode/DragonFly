using DragonFly.AspNet.Options;
using DragonFly.AspNetCore.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNet.Middleware;

class RequireAuthentificationMiddleware
{
    private readonly RequestDelegate _next;


    public RequireAuthentificationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == false)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        else
        {
            await _next(context);
        }
    }
}
