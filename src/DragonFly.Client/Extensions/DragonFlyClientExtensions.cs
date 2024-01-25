// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using BlazorStrap;
using DragonFly.Core;
using DragonFly.Init;
using DragonFly.Client.Builders;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
    /// <see cref="ComponentManager"/>, <see cref="FieldManager"/>, <see cref="AssetMetadataManager"/>, <see cref="AssetPreviewManager"/>
    /// <br/><br/>
    /// Default modules:<br/>
    /// <see cref="ContentModule"/>, <see cref="AssetModule"/>, <see cref="WebHookModule"/>, <see cref="BackgroundTaskModule"/>, <see cref="SettingsModule"/>
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddDragonFly(this WebAssemblyHostBuilder builder)
    {
        var uri = new Uri(builder.HostEnvironment.BaseAddress);

        return builder.AddDragonFly(new Uri($"{uri.Scheme}://{uri.Authority}/dragonfly/"), uri);
    }

    /// <summary>
    /// Adds DragonFly services.
    /// <br /><br />
    /// Default fields: <br />
    /// <see cref="BoolField"/>, <see cref="StringField"/>, <see cref="SlugField"/>, <see cref="TextField"/>, <see cref="IntegerField"/>, <see cref="FloatField"/>, <see cref="HtmlField"/>, <see cref="ColorField"/>, <see cref="GeolocationField"/><br />
    /// <see cref="AssetField"/>, <see cref="ReferenceField"/>
    /// <br /><br />
    /// Default asset metadata: <br/>
    /// <see cref="ImageMetadata"/>, <see cref="PdfMetadata"/><br /><br />
    /// Default services::<br/>
    /// <see cref="ISlugService"/> -> <see cref="SlugService"/><br />
    /// </summary>
    private static IDragonFlyBuilder AddDragonFly(this WebAssemblyHostBuilder hostBuilder, Uri apiBaseUri, Uri clientBaseUrl)
    {
        IDragonFlyBuilder builder = new DragonFlyBuilder(hostBuilder.Services);
        builder
            .AddCore()
            .AddRazorRouting()
            .Init(api =>
            {
                api.Module().Add<ContentModule>();
                api.Module().Add<AssetModule>();
                api.Module().Add<WebHookModule>();
                api.Module().Add<BackgroundTaskModule>();
                api.Module().Add<SettingsModule>();
            })
            .PostInit(api =>
            {
                foreach (ClientModule module in api.Module().Modules)
                {
                    module.Init(api);
                }
            });

        builder.Services.AddBlazorStrap();
        
        builder.Services.AddSingleton(ComponentManager.Default);
        builder.Services.AddSingleton(AssetPreviewManager.Default);

        builder.Services.AddSingleton(new DragonFlyApp(apiBaseUri, clientBaseUrl));
        builder.Services.AddSingleton(new HttpClient() { BaseAddress = apiBaseUri });

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

        return builder;
    }

    /// <summary>
    /// Initializes the DragonFly API by executing the following services: <see cref="IPreInitialize"/>, <see cref="IInitialize"/> and <see cref="IPostInitialize"/>.
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static async Task InitDragonFlyAsync(this WebAssemblyHost host)
    {
        var api = host.Services.GetRequiredService<IDragonFlyApi>();
        await api.InitAsync();
    }
}
