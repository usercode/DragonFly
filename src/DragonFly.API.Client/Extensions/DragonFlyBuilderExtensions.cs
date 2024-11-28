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
    /// <see cref="IContentStorage"/> -> <see cref="ContentItemApiStorage"/><br />
    /// <see cref="IContentVersionStorage"/> -> <see cref="ContentVersionApiStorage"/><br />
    /// <see cref="ISchemaStorage"/> -> <see cref="ContentSchemaApiStorage"/><br />
    /// <see cref="IAssetStorage"/> -> <see cref="AssetApiStorage"/><br />
    /// <see cref="IAssetFolderStorage"/> -> <see cref="AssetFolderApiStorage"/><br />
    /// <see cref="IWebHookStorage"/> -> <see cref="WebHookApiStorage"/><br />
    /// <see cref="IBackgroundTaskService"/> -> <see cref="BackgroundTaskApiStorage"/>
    /// </summary>
    public static IDragonFlyBuilder AddRestClient(this IDragonFlyBuilder builder)
    {
        builder.AddClientRestApiCore((provider, client) =>
        {
            DragonFlyApp app = provider.GetRequiredService<DragonFlyApp>();

            client.BaseAddress = app.ApiBaseUrl;
        });

        return builder;
    }
}
