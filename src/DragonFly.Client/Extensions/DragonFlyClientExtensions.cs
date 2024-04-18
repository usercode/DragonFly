// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Core;
using DragonFly.Client.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using DragonFly.AspNetCore.Permissions;

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
    public static IDragonFlyBuilder AddDragonFlyClient(this IServiceCollection services)
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
           ;

        builder.Services.AddBlazorStrap();
        
        builder.Services.TryAddSingleton(ComponentManager.Default);
        builder.Services.TryAddSingleton(AssetPreviewManager.Default);

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, BlazorClientAuthenticationStateProvider>();

        return builder;
    }
}
