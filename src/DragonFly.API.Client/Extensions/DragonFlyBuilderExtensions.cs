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
    /// <see cref="IContentStorage"/> -> <see cref="ClientContentService"/><br />
    /// <see cref="ISchemaStorage"/> -> <see cref="ClientContentService"/><br />
    /// <see cref="IAssetStorage"/> -> <see cref="ClientContentService"/><br />
    /// <see cref="IAssetFolderStorage"/> -> <see cref="ClientContentService"/><br />
    /// <see cref="IWebHookStorage"/> -> <see cref="ClientContentService"/><br />
    /// <see cref="IBackgroundTaskService"/> -> <see cref="ClientContentService"/>
    /// </summary>
    public static IDragonFlyBuilder AddRestClient(this IDragonFlyBuilder builder)
    {
        builder.AddRestApiCore();

        builder.Services.AddTransient<ClientContentService>();
        builder.Services.AddTransient<IContentStorage, ClientContentService>();
        builder.Services.AddTransient<ISchemaStorage, ClientContentService>();
        builder.Services.AddTransient<IStructureStorage, ClientContentService>();
        builder.Services.AddTransient<IWebHookStorage, ClientContentService>();
        builder.Services.AddTransient<IAssetStorage, ClientContentService>();
        builder.Services.AddTransient<IAssetFolderStorage, ClientContentService>();
        builder.Services.AddTransient<IBackgroundTaskService, ClientContentService>();

        return builder;
    }
}
