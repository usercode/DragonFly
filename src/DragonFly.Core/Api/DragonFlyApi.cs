// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Init;

/// <summary>
/// DragonFlyApi
/// </summary>
public class DragonFlyApi : IDragonFlyApi
{
    public DragonFlyApi(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider ServiceProvider { get; }  
}
