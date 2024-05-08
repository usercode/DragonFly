// Copyright (c) usercode
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
    /// <see cref="IContentStorage"/> -> <see cref="ContentMongoStorage"/><br />
    /// <see cref="IContentVersionStorage"/> -> <see cref="ContentVersionMongoStorage"/><br />
    /// <see cref="ISchemaStorage"/> -> <see cref="SchemaMongoStorage"/><br />
    /// <see cref="IAssetStorage"/> -> <see cref="AssetMongoStorage"/><br />
    /// <see cref="IAssetFolderStorage"/> -> <see cref="AssetFolderMongoStorage"/><br />
    /// <see cref="IWebHookStorage"/> -> <see cref="WebHookMongoStorage"/><br />
    /// </summary>
    public static IDragonFlyBuilder AddMongoDbStorage(this IDragonFlyBuilder builder, Action<MongoDbOptions>? options = null)
    {
        if (options != null)
        {
            builder.Services.Configure(options);
        }

        builder.Services.AddSingleton<MongoClient>();
        builder.Services.AddTransient<IContentStorage, ContentMongoStorage>();
        builder.Services.AddTransient<IContentVersionStorage, ContentVersionMongoStorage>();
        builder.Services.AddTransient<ISchemaStorage, SchemaMongoStorage>();
        builder.Services.AddTransient<IAssetStorage, AssetMongoStorage>();
        builder.Services.AddTransient<IAssetFolderStorage, AssetFolderMongoStorage>();
        builder.Services.AddTransient<IWebHookStorage, WebHookMongoStorage>();
        builder.Services.AddTransient<IStructureStorage, ContentStructureMongoStorage>();

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

        builder.PostInit<AssetIndexInitializer>();
        builder.PostInit<ContentIndexInitializer>();
        builder.PostInit<SchemaIndexInitializer>();

        ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack() { new IgnoreExtraElementsConvention(true) }, t => true);

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
