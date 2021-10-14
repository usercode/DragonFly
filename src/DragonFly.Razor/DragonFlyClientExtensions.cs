using Blazored.Modal;
using Blazored.Toast;
using BlazorStrap;
using DragonFly.Assets;
using DragonFly.Client.Core.Assets;
using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.Modules;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.Razor;
using DragonFly.Razor.Builder;
using DragonFly.Razor.Modules;
using DragonFly.Razor.Modules.Base;
using DragonFly.Razor.Pages.ContentItems.Fields;
using DragonFly.Razor.Pages.ContentItems.Query;
using DragonFly.Razor.Pages.ContentSchemas.Fields;
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
        public static IDragonFlyClientBuilder AddDragonFlyClient(this WebAssemblyHostBuilder builder)
        {
            var uri = new Uri(builder.HostEnvironment.BaseAddress);

            return AddDragonFlyClient(builder, new Uri( $"{uri.Scheme}://{uri.Authority}/dragonfly/"), uri);
        }

        private static IDragonFlyClientBuilder AddDragonFlyClient(this WebAssemblyHostBuilder builder, Uri apiBaseUri, Uri clientBaseUrl)
        {
            DragonFlyApp.Default.ApiBaseUrl = apiBaseUri;
            DragonFlyApp.Default.ClientBaseUrl = clientBaseUrl;

            //default modules
            DragonFlyApi.Default.RegisterModule<ContentModule>();
            DragonFlyApi.Default.RegisterModule<AssetModule>();
            DragonFlyApi.Default.RegisterModule<WebHookModule>();
            DragonFlyApi.Default.RegisterModule<SettingsModule>();

            builder.Services.AddSingleton(typeof(IDragonFlyApi), DragonFlyApi.Default);

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

            return new DragonFlyClientBuilder(builder);
        }

        public static void UseDragonFLyClient(this WebAssemblyHost host)
        {
            IDragonFlyApi api = host.Services.GetRequiredService<IDragonFlyApi>();
            api.Init();

            foreach (ClientModule module in api.Module().Modules)
            {
                module.Init(DragonFlyApi.Default);
            }            
        }
    }
}