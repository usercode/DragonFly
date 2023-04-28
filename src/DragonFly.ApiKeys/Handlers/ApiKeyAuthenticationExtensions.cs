// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.ApiKeys.Handlers;

public static class ApiKeyAuthenticationExtensions
{
    public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder, string scheme, Action<ApiKeyAuthenticationOptions>? configureOptions = null)
    {
        builder.Services.AddOptions<ApiKeyAuthenticationOptions>(scheme);
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(scheme, null, configureOptions);
    }
}
