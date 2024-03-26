// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// DragonFlyApi
/// </summary>
public sealed class DragonFlyApi : IDragonFlyApi
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static IDragonFlyApi Default { get; internal set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public DragonFlyApi(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider ServiceProvider { get; }
}
