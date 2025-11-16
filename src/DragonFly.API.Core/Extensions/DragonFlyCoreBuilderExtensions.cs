// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.API.Client;
using DragonFly.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Core;

public static class DragonFlyCoreBuilderExtensions
{
    public static TDragonFlyBuilder AddRestApiCore<TDragonFlyBuilder>(this TDragonFlyBuilder builder)
        where TDragonFlyBuilder : IDragonFlyBuilder
    {
        builder.PreInit(api =>
        {
            api.Fields.Added += factory => JsonFieldManager.Default.EnsureField(factory.FieldType);
        });

        builder.Init(api =>
        {
            api.JsonFields.AddDefaults();
        });

        return builder;
    }

    /// <summary>
    /// Adds client API for storages.
    /// </summary>
    public static TDragonFlyBuilder AddClientRestApiCore<TDragonFlyBuilder>(this TDragonFlyBuilder builder, Action<IServiceProvider, HttpClient>? configureClient = null)
        where TDragonFlyBuilder : IDragonFlyBuilder
    {
        builder.AddRestApiCore();

        if (configureClient == null)
        {
            configureClient = (provider, client) => { };
        }

        builder.Services.AddHttpClient<RestApiClient>(configureClient);

        builder.Services.AddTransient<IContentStorage, ContentItemApiStorage>();
        builder.Services.AddTransient<IContentVersionStorage, ContentVersionApiStorage>();
        builder.Services.AddTransient<ISchemaStorage, ContentSchemaApiStorage>();
        builder.Services.AddTransient<IStructureStorage, ContentStructureApiStorage>();
        builder.Services.AddTransient<IWebHookStorage, WebHookApiStorage>();
        builder.Services.AddTransient<IAssetStorage, AssetApiStorage>();
        builder.Services.AddTransient<IAssetFolderStorage, AssetFolderApiStorage>();
        builder.Services.AddTransient<IBackgroundTaskService, BackgroundTaskApiStorage>();

        return builder;
    }
}
