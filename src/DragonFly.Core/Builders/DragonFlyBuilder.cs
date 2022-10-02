// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Builders;

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
