// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using DragonFly.Client.Builders;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DragonFly.Client;

public static class DragonFlyClientWebAssemblyExtensions
{
    public static Uri? BaseHttpBaseAddress = null;

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
    public static WebAssemblyHostBuilder AddDragonFlyClient(this WebAssemblyHostBuilder webAssemblyBuilder, Action<IDragonFlyBuilder>? config = null)
    {
        var clientBaseUrl = new Uri(webAssemblyBuilder.HostEnvironment.BaseAddress);
        var apiBaseUri = new Uri($"{clientBaseUrl.Scheme}://{clientBaseUrl.Authority}/dragonfly/");

        webAssemblyBuilder.Services.AddDragonFlyClient(config);

        webAssemblyBuilder.Services.AddSingleton(new DragonFlyApp(apiBaseUri, clientBaseUrl));
        webAssemblyBuilder.Services.AddSingleton(new HttpClient() { BaseAddress = apiBaseUri });

        BaseHttpBaseAddress = apiBaseUri;

        return webAssemblyBuilder;
    }

    /// <summary>
    /// Initializes the DragonFly API by executing the following services: <see cref="IPreInitialize"/>, <see cref="IInitialize"/> and <see cref="IPostInitialize"/>.
    /// </summary>
    public static async Task InitDragonFlyAsync(this WebAssemblyHost host)
    {
        var api = host.Services.GetRequiredService<IDragonFlyApi>();
        await api.InitAsync();
    }
}
