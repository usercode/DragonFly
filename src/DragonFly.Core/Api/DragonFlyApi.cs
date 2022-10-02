﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

/// <summary>
/// DragonFlyApi
/// </summary>
public class DragonFlyApi : IDragonFlyApi
{
    public DragonFlyApi(IServiceProvider serviceProvider)
    {
        _done = false;
        ServiceProvider = serviceProvider;
    }
    
    public IServiceProvider ServiceProvider { get; }

    private bool _done;

    public async Task InitAsync()
    {
        if (_done)
        {
            throw new Exception("The DragonFlyApi is already initialized.");
        }

        using IServiceScope scope = ServiceProvider.CreateScope();

        foreach (IPreInitialize item in scope.ServiceProvider.GetServices<IPreInitialize>())
        {
            await item.ExecuteAsync(this);
        }

        foreach (IInitialize item in scope.ServiceProvider.GetServices<IInitialize>())
        {
            await item.ExecuteAsync(this);
        }

        foreach (IPostInitialize item in scope.ServiceProvider.GetServices<IPostInitialize>())
        {
            await item.ExecuteAsync(this);
        }

        _done = true;
    }
}
