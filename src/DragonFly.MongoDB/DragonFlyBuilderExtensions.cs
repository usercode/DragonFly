﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds MongoDB storage service.<br />
    /// <br />
    /// Default services:<br />
    /// <see cref="IDataStorage"/> -> <see cref="MongoStorage"/><br />
    /// <see cref="IContentStorage"/> -> <see cref="MongoStorage"/><br />
    /// <see cref="ISchemaStorage"/> -> <see cref="MongoStorage"/><br />
    /// <see cref="IAssetStorage"/> -> <see cref="MongoStorage"/><br />
    /// <see cref="IAssetFolderStorage"/> -> <see cref="MongoStorage"/><br />
    /// <see cref="IWebHookStorage"/> -> <see cref="MongoStorage"/><br />
    /// </summary>
    public static IDragonFlyBuilder AddMongoDbStorage(this IDragonFlyBuilder builder, Action<MongoDbOptions>? options = null)
    {
        if (options != null)
        {
            builder.Services.Configure(options);
        }

        builder.Services.AddSingleton<MongoStorage>();
        builder.Services.AddSingleton<IDataStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IContentStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<ISchemaStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IStructureStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IAssetStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IAssetFolderStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IWebHookStorage>(x => x.GetRequiredService<MongoStorage>());

        builder.Services.AddSingleton(MongoFieldManager.Default);
        builder.Services.AddSingleton(MongoQueryManager.Default);
        builder.Services.AddSingleton(MongoIndexManager.Default);

        //fix for nested field options (inside ArrayFieldOptions)
        builder.PreInit(api => api.ContentFields().Added += ContentFieldAdded);

        builder.Init(api => api.MongoFields().AddDefaults());

        builder.PostInit<CreateIndexAction>();

        return builder;
    }

    private static void ContentFieldAdded(Type contentFieldType, FieldOptionsAttribute? fieldOptionsAttribute, FieldQueryAttribute? fieldQueryAttribute)
    {
        if (fieldOptionsAttribute != null)
        {
            BsonClassMap map = new BsonClassMap(fieldOptionsAttribute.OptionsType);
            map.AutoMap();

            BsonClassMap.RegisterClassMap(map);
        }
    }
}
