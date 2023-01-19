// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Blazored.Toast;
using BlazorStrap;
using DragonFly.API;
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
    public static IDragonFlyBuilder AddDragonFly(this WebAssemblyHostBuilder builder)
    {
        var uri = new Uri(builder.HostEnvironment.BaseAddress);

        return builder.AddDragonFly(new Uri($"{uri.Scheme}://{uri.Authority}/dragonfly/"), uri);
    }

    private static IDragonFlyBuilder AddDragonFly(this WebAssemblyHostBuilder hostBuilder, Uri apiBaseUri, Uri clientBaseUrl)
    {
        var builder = new DragonFlyBuilder(hostBuilder.Services);

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
        builder.Services.AddSingleton(AssetPreviewManager.Default);

        builder.Services.AddSingleton<ISlugService, SlugService>();

        builder.Services.AddTransient(sp => new HttpClient { BaseAddress = apiBaseUri });

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

        //add default modules
        builder.Init(api =>
        {
            api.Module().Add<ContentModule>();
            api.Module().Add<AssetModule>();
            api.Module().Add<WebHookModule>();
            api.Module().Add<SettingsModule>();
        });

        //init all modules
        builder.PostInit(api =>
        {
            foreach (ClientModule module in api.Module().Modules)
            {
                module.Init(api);
            }
        });

        return builder;
    }

    public static async Task InitDragonFly(this WebAssemblyHost host)
    {
        var api = host.Services.GetRequiredService<IDragonFlyApi>();
        await api.InitAsync();
    }
}
