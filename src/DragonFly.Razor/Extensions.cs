﻿using Blazored.Modal;
using Blazored.Toast;
using BlazorStrap;
using DragonFly.Assets;
using DragonFly.Client.Core.Assets;
using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.Razor.Builder;
using DragonFly.Razor.Options;
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
    public static class Extensions
    {
        public static IDragonFlyClientBuilder AddDragonFlyClient(this WebAssemblyHostBuilder builder)
        {
            var uri = new Uri(builder.HostEnvironment.BaseAddress);

            return AddDragonFlyClient(builder, new Uri( $"{uri.Scheme}://{uri.Authority}/dragonfly/"), uri);
        }

        private static IDragonFlyClientBuilder AddDragonFlyClient(this WebAssemblyHostBuilder builder, Uri apiBaseUri, Uri clientBaseUrl)
        {
            builder.Services.AddSingleton(new DragonFlyClientContext() 
            { 
                ApiBaseUrl = apiBaseUri, 
                ClientBaseUrl = clientBaseUrl 
            });

            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazoredToast();
            
            builder.Services.AddBootstrapCss();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = apiBaseUri });
            builder.Services.AddTransient<ClientContentService>();
            builder.Services.AddTransient<IContentStorage, ClientContentService>();
            builder.Services.AddTransient<IWebHookStorage, ClientContentService>();
            builder.Services.AddTransient<IAssetStorage, ClientContentService>();
            builder.Services.AddTransient<IAssetFolderStorage, ClientContentService>();

            builder.Services.AddTransient<IImageAssetUrlService, DefaultAssetDataUrlService>();
            builder.Services.AddTransient<IImageAssetUrlService, ImageWizardAssetDataUrlService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            builder.Services.AddSingleton(ComponentManager.Default);
            builder.Services.AddSingleton(ContentFieldManager.Default);
            builder.Services.AddSingleton(AssetMetadataManager.Default);

            ComponentManager.Default.RegisterField<ArrayFieldView>();
            ComponentManager.Default.RegisterField<AssetFieldView>();
            ComponentManager.Default.RegisterField<BoolFieldView>();
            ComponentManager.Default.RegisterField<DateTimeFieldView>();
            ComponentManager.Default.RegisterField<EmbedFieldView>();
            ComponentManager.Default.RegisterField<FloatFieldView>();
            ComponentManager.Default.RegisterField<IntegerFieldView>();
            ComponentManager.Default.RegisterField<ReferenceFieldView>();
            ComponentManager.Default.RegisterField<SlugFieldView>();
            ComponentManager.Default.RegisterField<StringFieldView>();
            ComponentManager.Default.RegisterField<TextAreaFieldView>();
            ComponentManager.Default.RegisterField<XHtmlFieldView>();
            ComponentManager.Default.RegisterField<XmlFieldView>();

            ComponentManager.Default.RegisterOptions<ArrayFieldOptionsView>();
            ComponentManager.Default.RegisterOptions<AssetFieldOptionsView>();
            ComponentManager.Default.RegisterOptions<BoolFieldOptionsView>();
            ComponentManager.Default.RegisterOptions<EmbedFieldOptionsView>();
            ComponentManager.Default.RegisterOptions<FloatFieldOptionsView>();
            ComponentManager.Default.RegisterOptions<IntegerFieldOptionsView>();
            ComponentManager.Default.RegisterOptions<StringFieldOptionsView>();
            ComponentManager.Default.RegisterOptions<ReferenceFieldOptionsView>();

            ComponentManager.Default.RegisterAssetMetadata<ImageMetadataView>();
            ComponentManager.Default.RegisterAssetMetadata<PdfMetadataView>();

            ComponentManager.Default.RegisterQuery<StringFieldQueryView>();
            ComponentManager.Default.RegisterQuery<IntegerFieldQueryView>();
            ComponentManager.Default.RegisterQuery<ReferenceFieldQueryView>();
            ComponentManager.Default.RegisterQuery<AssetFieldQueryView>();

            return new DragonFlyClientBuilder(builder);
        }
    }
}