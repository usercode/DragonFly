﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Core;
using DragonFly.Client.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Microsoft.FluentUI.AspNetCore.Components;
using DragonFly.Client.Pages.BackgroundTasks;

namespace DragonFly.Client;

public static class DragonFlyClientExtensions
{
    /// <summary>
    /// Adds DragonFly services.
    /// <br/><br/>
    /// Default services:<br/>
    /// <see cref="ISlugService"/> -> <see cref="SlugService"/><br/>
    /// <br/>
    /// Default manager:<br/>
    /// <see cref="ComponentManager"/>, <see cref="FieldManager"/>, <see cref="MetadataManager"/>, <see cref="AssetPreviewManager"/>
    /// <br/><br/>
    /// Default modules:<br/>
    /// <see cref="ContentInitializer"/>, <see cref="AssetInitializer"/>, <see cref="WebHookInitializer"/>, <see cref="BackgroundTaskInitializer"/>, <see cref="SettingsInitializer"/>
    /// </summary>
    public static IServiceCollection AddDragonFlyClient(this IServiceCollection services, Action<IDragonFlyBuilder>? config = null)
    {
        IDragonFlyBuilder builder = new DragonFlyBuilder(services);
        builder
            .AddCore()
            .AddRazorRouting()
            .Init<ContentInitializer>()
            .Init<AssetInitializer>()
            .Init<WebHookInitializer>()
            .Init<BackgroundTaskInitializer>()
            .Init<SettingsInitializer>()
            .PostInit(async x =>
            {
                //var toastService = x.ServiceProvider.GetRequiredService<IToastService>();
                //var background = x.ServiceProvider.GetRequiredService<BackgroundTaskComponent>();

                //toastService.ShowInfo("Ready");
                
                //await background.InitAsync();


            })
           ;

        builder.Services.AddAuthorizationCore();
        builder.Services.AddBlazorStrap();
        builder.Services.AddFluentUIComponents();
        
        builder.Services.TryAddSingleton(ComponentManager.Default);
        builder.Services.TryAddSingleton(AssetPreviewManager.Default);

        //builder.Services.AddScoped<IPrincipalContext>(x => x.GetRequiredService<BlazorClientAuthenticationStateProvider>());

        config?.Invoke(builder);

        return services;
    }
}
