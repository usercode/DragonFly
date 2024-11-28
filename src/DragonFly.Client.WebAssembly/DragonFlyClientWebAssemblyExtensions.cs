// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using DragonFly.Client.Builders;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.AspNetCore.Permissions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace DragonFly.Client;

public static class DragonFlyClientWebAssemblyExtensions
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
    public static WebAssemblyHostBuilder AddDragonFlyClient(this WebAssemblyHostBuilder webAssemblyBuilder, Action<IDragonFlyBuilder>? config = null)
    {
        var clientBaseUrl = new Uri(webAssemblyBuilder.HostEnvironment.BaseAddress);
        var apiBaseUri = new Uri($"{clientBaseUrl.Scheme}://{clientBaseUrl.Authority}/dragonfly/");

        webAssemblyBuilder.Services.AddDragonFlyClient(config);

        webAssemblyBuilder.Services.AddSingleton(new DragonFlyApp(apiBaseUri, clientBaseUrl));
        webAssemblyBuilder.Services.AddSingleton(new HttpClient() { BaseAddress = apiBaseUri });

        webAssemblyBuilder.Services.AddScoped<BlazorClientAuthenticationStateProvider>();
        webAssemblyBuilder.Services.AddScoped<AuthenticationStateProvider>(x => x.GetRequiredService<BlazorClientAuthenticationStateProvider>());

        DragonFlyRenderMode.Current = new InteractiveWebAssemblyRenderMode(prerender: false);

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
