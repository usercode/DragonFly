﻿using Blazored.Modal;
using Blazored.Toast;
using BlazorStrap;
using DragonFly.Assets;
using DragonFly.Client.Core.Assets;
using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Builders;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.Modules;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.Razor;
using DragonFly.Razor.Modules;
using DragonFly.Razor.Modules.Base;
using DragonFly.Razor.Pages.ContentItems.Fields;
using DragonFly.Razor.Pages.ContentItems.Query;
using DragonFly.Razor.Pages.ContentSchemas.Fields;
using DragonFly.Razor.Pages.Settings;
using DragonFly.Razor.Pages.Settings.Modules;
using DragonFly.Razor.Services;
using DragonFly.Razor.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core
{
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

            DragonFlyApp.Default.ApiBaseUrl = apiBaseUri;
            DragonFlyApp.Default.ClientBaseUrl = clientBaseUrl;

            builder.Services.AddSingleton<IDragonFlyApi, DragonFlyApi>();

            builder.Services.AddSingleton(DragonFlyApp.Default);
            builder.Services.AddSingleton(ComponentManager.Default);
            builder.Services.AddSingleton(ContentFieldManager.Default);
            builder.Services.AddSingleton(AssetMetadataManager.Default);

            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazoredToast();

            builder.Services.AddBootstrapCss();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = apiBaseUri });
            builder.Services.AddTransient<ClientContentService>();
            builder.Services.AddTransient<IContentStorage, ClientContentService>();
            builder.Services.AddTransient<IStructureStorage, ClientContentService>();
            builder.Services.AddTransient<IWebHookStorage, ClientContentService>();
            builder.Services.AddTransient<IAssetStorage, ClientContentService>();
            builder.Services.AddTransient<IAssetFolderStorage, ClientContentService>();

            builder.Services.AddTransient<IImageAssetUrlService, DefaultAssetDataUrlService>();
            builder.Services.AddTransient<IImageAssetUrlService, ImageWizardAssetDataUrlService>();

            builder.Services.AddSingleton<IDragonFlyModule, AssetModule>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            //default modules
            builder.Init(api =>
            {
                api.RegisterModule<ContentModule>();
                api.RegisterModule<AssetModule>();
                api.RegisterModule<WebHookModule>();
                api.RegisterModule<SettingsModule>();

                api.AssetMetadata().Register<ImageMetadata>();
                api.AssetMetadata().Register<PdfMetadata>();
            });

            return builder;
        }

        public static void UseDragonFLyClient(this WebAssemblyHost host)
        {
            IDragonFlyApi api = host.Services.GetRequiredService<IDragonFlyApi>();
            api.InitAsync().GetAwaiter().GetResult();

            foreach (ClientModule module in api.Module().Modules)
            {
                module.Init(api);
            }
        }
    }
}