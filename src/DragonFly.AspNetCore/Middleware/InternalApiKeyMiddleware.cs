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

class InternalApiKeyMiddleware
{
    public const string ApiKeyHeaderName = "ApiKey";

    private readonly RequestDelegate _next;


    public InternalApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context, DragonFlyContext dragonFlyContext)
    {
        string? requestApiKey = context.Request.Headers[ApiKeyHeaderName].FirstOrDefault();

        if (requestApiKey == dragonFlyContext.Options.ApiKey)
        {
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, ApiKeyHeaderName) }, "apikey"));
        }

        return _next(context);
    }
}
