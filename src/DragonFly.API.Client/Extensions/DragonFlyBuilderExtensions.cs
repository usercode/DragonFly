// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API.Client;
using DragonFly.Client.Builders;
using DragonFly.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Client;

public static class StartupExtensions
{
    /// <summary>
    /// Adds storages for REST.<br />
    /// <br />
    /// Default services:<br />
    /// <see cref="IContentStorage"/> -> <see cref="ContentApiStorage"/><br />
    /// <see cref="IContentVersionStorage"/> -> <see cref="ContentVersionApiStorage"/><br />
    /// <see cref="ISchemaStorage"/> -> <see cref="SchemaApiStorage"/><br />
    /// <see cref="IAssetStorage"/> -> <see cref="AssetApiStorage"/><br />
    /// <see cref="IAssetFolderStorage"/> -> <see cref="AssetFolderApiStorage"/><br />
    /// <see cref="IWebHookStorage"/> -> <see cref="WebHookApiStorage"/><br />
    /// <see cref="IBackgroundTaskService"/> -> <see cref="BackgroundTaskApiStorage"/>
    /// </summary>
    public static IDragonFlyBuilder AddRestClient(this IDragonFlyBuilder builder)
    {
        builder.AddRestApiCore();

        builder.Services.AddTransient<IContentStorage, ContentApiStorage>();
        builder.Services.AddTransient<IContentVersionStorage, ContentVersionApiStorage>();
        builder.Services.AddTransient<ISchemaStorage, SchemaApiStorage>();
        builder.Services.AddTransient<IStructureStorage, ContentStructureApiStorage>();
        builder.Services.AddTransient<IWebHookStorage, WebHookApiStorage>();
        builder.Services.AddTransient<IAssetStorage, AssetApiStorage>();
        builder.Services.AddTransient<IAssetFolderStorage, AssetFolderApiStorage>();
        builder.Services.AddTransient<IBackgroundTaskService, BackgroundTaskApiStorage>();

        return builder;
    }
}
