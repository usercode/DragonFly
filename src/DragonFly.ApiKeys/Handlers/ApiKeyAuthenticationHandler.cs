// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DragonFly.ApiKeys.Handlers;

internal class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    public ApiKeyAuthenticationHandler(
                                IOptionsMonitor<ApiKeyAuthenticationOptions> options, 
                                ILoggerFactory logger, 
                                UrlEncoder encoder, 
                                ISystemClock clock,
                                IApiKeyService apiKeyService) 
        : base(options, logger, encoder, clock)
    {
        ApiKeyService = apiKeyService;
    }

    /// <summary>
    /// ApiKeyService
    /// </summary>
    private IApiKeyService ApiKeyService { get; }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? requestApiKey = Context.Request.Headers["ApiKey"].FirstOrDefault();

        if (requestApiKey != null)
        {
            ApiKey? apiKey = await ApiKeyService.GetApiKey(requestApiKey);

            if (apiKey != null)
            {
                return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("Name", $"apikey:{apiKey.Name}"), new Claim("ApiKeyId", apiKey.Id.ToString()) }, "ApiKey")), Scheme.Name));
            }
            else
            {
                return AuthenticateResult.Fail("Invalid api key");
            }
        }

        return AuthenticateResult.NoResult();
    }
}
