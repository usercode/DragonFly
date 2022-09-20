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
