// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DragonFly.Init;

public static class DragonFlyApiExtensions
{
    public static async Task InitAsync(this IDragonFlyApi api)
    {
        DragonFlyApi.Default = api;

        ILogger<DragonFlyApi> logger = api.ServiceProvider.GetRequiredService<ILogger<DragonFlyApi>>();

        using IServiceScope scope = api.ServiceProvider.CreateScope();

        foreach (IPreInitialize item in scope.ServiceProvider.GetServices<IPreInitialize>())
        {
            logger.LogInformation("Initializing {type}", GetName(item));

            await item.ExecuteAsync(api);
        }

        foreach (IInitialize item in scope.ServiceProvider.GetServices<IInitialize>())
        {
            logger.LogInformation("Initializing {type}", GetName(item));

            await item.ExecuteAsync(api);
        }

        foreach (IPostInitialize item in scope.ServiceProvider.GetServices<IPostInitialize>())
        {
            logger.LogInformation("Initializing {type}", GetName(item));

            await item.ExecuteAsync(api);
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
            return initializer.GetType().Name;
        }
    }
}
