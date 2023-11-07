// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DragonFly.Init;

/// <summary>
/// DragonFlyApi
/// </summary>
public class DragonFlyApi : IDragonFlyApi
{
    public DragonFlyApi(IServiceProvider serviceProvider, ILogger<DragonFlyApi> logger)
    {
        _done = false;
        ServiceProvider = serviceProvider;
        Logger = logger;
    }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<DragonFlyApi> Logger { get; }

    private bool _done;

    public async Task InitAsync()
    {
        if (_done)
        {
            throw new Exception("The DragonFly API is already initialized.");
        }

        _done = true;

        using IServiceScope scope = ServiceProvider.CreateScope();

        foreach (IPreInitialize item in scope.ServiceProvider.GetServices<IPreInitialize>())
        {
            Logger.LogInformation("Initializing {type}", GetName(item));

            await item.ExecuteAsync(this);
        }

        foreach (IInitialize item in scope.ServiceProvider.GetServices<IInitialize>())
        {
            Logger.LogInformation("Initializing {type}", GetName(item));

            await item.ExecuteAsync(this);
        }

        foreach (IPostInitialize item in scope.ServiceProvider.GetServices<IPostInitialize>())
        {
            Logger.LogInformation("Initializing {type}", GetName(item));

            await item.ExecuteAsync(this);
        }
    }

    private static string? GetName(object initializer)
    {
        if (initializer is InitializerItem item)
        {
            return item.Name;
        }
        else
        {
            return initializer.GetType().FullName;
        }
    }
}
