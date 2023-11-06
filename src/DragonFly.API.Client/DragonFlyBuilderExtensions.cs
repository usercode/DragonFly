﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.API.Client;
using DragonFly.Client.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Client;

public static class StartupExtensions
{
    public static IDragonFlyBuilder AddRestApi(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<ClientContentService>();
        builder.Services.AddTransient<IContentStorage, ClientContentService>();
        builder.Services.AddTransient<ISchemaStorage, ClientContentService>();
        builder.Services.AddTransient<IStructureStorage, ClientContentService>();
        builder.Services.AddTransient<IWebHookStorage, ClientContentService>();
        builder.Services.AddTransient<IAssetStorage, ClientContentService>();
        builder.Services.AddTransient<IAssetFolderStorage, ClientContentService>();
        builder.Services.AddTransient<IBackgroundTaskService, ClientContentService>();

        builder.Init(api =>
        {
            api.JsonFields().AddDefaults();
            api.ContentField().Added += fieldFactory => JsonFieldManager.Default.EnsureField(fieldFactory.FieldType);
        });

        return builder;
    }
}
