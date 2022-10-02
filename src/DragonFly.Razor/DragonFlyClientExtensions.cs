// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Blazored.Toast;
using BlazorStrap;
using DragonFly.Builders;
using DragonFly.Core.ContentStructures;
using DragonFly.Razor;
using DragonFly.Razor.Modules;
using DragonFly.Razor.Services;
using DragonFly.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace DragonFly.Client.Core;

public static class DragonFlyClientExtensions
{
    public static IDragonFlyBuilder AddDragonFlyClient(this WebAssemblyHostBuilder builder)
    {
        var uri = new Uri(builder.HostEnvironment.BaseAddress);

        return AddDragonFlyClient(builder, new Uri( $"{uri.Scheme}://{uri.Authority}/dragonfly/"), uri);
    }

    private static IDragonFlyBuilder AddDragonFlyClient(this WebAssemblyHostBuilder hostBuilder, Uri apiBaseUri, Uri clientBaseUrl)
    {
        DragonFlyBuilder builder = new DragonFlyBuilder(hostBuilder.Services);

        builder.AddRazorRouting();

        builder.Services.AddBlazoredToast();

        builder.Services.AddBlazorStrap();

        DragonFlyApp.Default.ApiBaseUrl = apiBaseUri;
        DragonFlyApp.Default.ClientBaseUrl = clientBaseUrl;

        builder.Services.AddSingleton<IDragonFlyApi, DragonFlyApi>();

        builder.Services.AddSingleton(DragonFlyApp.Default);
        builder.Services.AddSingleton(ComponentManager.Default);
        builder.Services.AddSingleton(ContentFieldManager.Default);
        builder.Services.AddSingleton(AssetMetadataManager.Default);

        builder.Services.AddTransient(sp => new HttpClient { BaseAddress = apiBaseUri });
        builder.Services.AddTransient<ClientContentService>();
        builder.Services.AddTransient<IContentStorage, ClientContentService>();
        builder.Services.AddTransient<IStructureStorage, ClientContentService>();
        builder.Services.AddTransient<IWebHookStorage, ClientContentService>();
        builder.Services.AddTransient<IAssetStorage, ClientContentService>();
        builder.Services.AddTransient<IAssetFolderStorage, ClientContentService>();

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

        //default modules
        builder.Init(api =>
        {
            api.Module().Add<ContentModule>();
            api.Module().Add<AssetModule>();
            api.Module().Add<WebHookModule>();
            api.Module().Add<SettingsModule>();
        });

        return builder;
    }

    public static void UseDragonFlyClient(this WebAssemblyHost host)
    {
        IDragonFlyApi api = host.Services.GetRequiredService<IDragonFlyApi>();
        api.InitAsync().GetAwaiter().GetResult();

        foreach (ClientModule module in api.Module().Modules)
        {
            module.Init(api);
        }
    }
}
