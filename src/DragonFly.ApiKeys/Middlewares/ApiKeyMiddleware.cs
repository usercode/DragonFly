// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
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

    public async Task Invoke(HttpContext context)
    {
        string? requestApiKey = context.Request.Headers[ApiKeyHeaderName].FirstOrDefault();

        if (requestApiKey != null)
        {
            ApiKey apiKey = await ApiKeyService.GetApiKey(requestApiKey);

            if (apiKey != null)
            {
                PermissionState.SetPrincipal(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("Name", $"apikey:{apiKey.Name}"), new Claim("ApiKeyId", apiKey.Id.ToString()) }, "ApiKey")));
            }
        }

        await _next(context);
    }
}
