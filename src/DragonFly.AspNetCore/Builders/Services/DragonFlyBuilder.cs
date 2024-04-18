// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authentication;

namespace DragonFly.AspNetCore.Builders;

/// <summary>
/// DragonFlyBuilder
/// </summary>
public class DragonFlyBuilder : IDragonFlyBuilder
{
    public DragonFlyBuilder(IServiceCollection services)
    {
        Services = services;
    }

    /// <summary>
    /// Services
    /// </summary>
    public IServiceCollection Services { get; }
}
