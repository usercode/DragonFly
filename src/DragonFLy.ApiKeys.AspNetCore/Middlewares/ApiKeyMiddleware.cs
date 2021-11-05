﻿using DragonFly.AspNet.Options;
using DragonFly.AspNetCore.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Middlewares;

class ApiKeyMiddleware
{
    public const string ApiKeyHeaderName = "ApiKey";

    private readonly RequestDelegate _next;


    public ApiKeyMiddleware(RequestDelegate next, IApiKeyService apiKeyService)
    {
        _next = next;

        ApiKeyService = apiKeyService;
    }

    private IApiKeyService ApiKeyService { get; }

    public async Task Invoke(HttpContext context, DragonFlyContext dragonFlyContext)
    {
        string? requestApiKey = context.Request.Headers[ApiKeyHeaderName].FirstOrDefault();

        if (requestApiKey != null)
        {
            ApiKey apiKey = await ApiKeyService.GetApiKey(requestApiKey);

            if (apiKey != null)
            {
                context.User = new ClaimsPrincipal(new ClaimsIdentity(new [] { new Claim("ApiKeyId", apiKey.Id.ToString()) }, "ApiKey"));
            }
        }

        await _next(context);
    }
}
