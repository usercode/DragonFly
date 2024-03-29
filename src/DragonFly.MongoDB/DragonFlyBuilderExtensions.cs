﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds MongoDB storage service.<br />
    /// <br />
    /// Default services:<br />
    /// <see cref="IContentStorage"/> -> <see cref="MongoStorage"/><br />
    /// <see cref="IContentVersionStorage"/> -> <see cref="MongoStorage"/><br />
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
        builder.Services.AddSingleton<IContentStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IContentVersionStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<ISchemaStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IStructureStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IAssetStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IAssetFolderStorage>(x => x.GetRequiredService<MongoStorage>());
        builder.Services.AddSingleton<IWebHookStorage>(x => x.GetRequiredService<MongoStorage>());

        builder.Services.AddSingleton(MongoFieldManager.Default);
        builder.Services.AddSingleton(MongoIndexManager.Default);
        builder.Services.AddSingleton(MongoQueryManager.Default);

        builder.PreInit(api =>
        {
            api.Field().Added += factory => CreateFieldOptionsBsonClassMap(factory); //fix for nested field options (inside ArrayFieldOptions)
            api.Field().Added += factory => MongoFieldManager.Default.EnsureField(factory);
        });

        builder.Init(api =>
        {
            api.MongoField().AddDefaults();
            api.MongoIndex().AddDefaults();
            api.MongoQuery().AddDefaults();
        });

        builder.PostInit<CreateIndexInitializer>();

        var pack = new ConventionPack()
        {
            new IgnoreExtraElementsConvention(true)
        };
        ConventionRegistry.Register("IgnoreExtraElements", pack, t => true);

        return builder;
    }

    private static void CreateFieldOptionsBsonClassMap(FieldFactory fieldFactory)
    {
        if (fieldFactory.OptionsType != null)
        {
            if (BsonClassMap.IsClassMapRegistered(fieldFactory.OptionsType))
            {
                return;
            }

            BsonClassMap map = new BsonClassMap(fieldFactory.OptionsType);
            map.AutoMap();

            BsonClassMap.RegisterClassMap(map);
        }
    }    
}
