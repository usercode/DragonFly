// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Entrypoint for DragonFly api.
/// </summary>
public interface IDragonFlyApi
{
    /// <summary>
    /// ServiceProvider
    /// </summary>
    IServiceProvider ServiceProvider { get; }
}
