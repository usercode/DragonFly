// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore.Builders;

/// <summary>
/// DragonFlyBuilder
/// </summary>
public class DragonFlyBuilder : IDragonFlyBuilder
{
    public DragonFlyBuilder(IServiceCollection services, AuthenticationBuilder authenticationBuilder)
    {
        Services = services;
        Authentication = authenticationBuilder;
    }

    /// <summary>
    /// Services
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// AuthenticationBuilder
    /// </summary>
    public AuthenticationBuilder Authentication { get; set; }
}
