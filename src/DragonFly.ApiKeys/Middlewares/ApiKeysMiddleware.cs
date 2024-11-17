// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DragonFly.ApiKeys.Middlewares;

internal class ApiKeysMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeysMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IApiKeyService apiService)
    {
        string? requestApiKey = httpContext.Request.Headers["ApiKey"].FirstOrDefault();

        if (requestApiKey != null)
        {
            ApiKey? apiKey = await apiService.GetApiKey(requestApiKey);

            if (apiKey != null)
            {
                httpContext.User = new ClaimsPrincipal(new ClaimsIdentity([
                                                                new Claim("Name", $"apikey:{apiKey.Name}"), 
                                                                new Claim("ApiKeyId", apiKey.Id.ToString())],
                                                                "ApiKey"));
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                return;
            }
        }

        await _next(httpContext);
    }
}
