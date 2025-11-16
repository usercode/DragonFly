// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFly.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds MongoDB storage service.<br />
    /// <br />
    /// Default services:<br />
    /// <see cref="IContentStorage"/> -> <see cref="ContentItemMongoStorage"/><br />
    /// <see cref="IContentVersionStorage"/> -> <see cref="ContentVersionMongoStorage"/><br />
    /// <see cref="ISchemaStorage"/> -> <see cref="ContentSchemaMongoStorage"/><br />
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

        builder.Services.TryAddSingleton<MongoClient>();
        builder.Services.AddTransient<IContentStorage, ContentItemMongoStorage>();
        builder.Services.AddTransient<IContentVersionStorage, ContentVersionMongoStorage>();
        builder.Services.AddTransient<ISchemaStorage, ContentSchemaMongoStorage>();
        builder.Services.AddTransient<IAssetStorage, AssetMongoStorage>();
        builder.Services.AddTransient<IAssetFolderStorage, AssetFolderMongoStorage>();
        builder.Services.AddTransient<IWebHookStorage, WebHookMongoStorage>();
        builder.Services.AddTransient<IStructureStorage, ContentStructureMongoStorage>();

        builder.Services.TryAddSingleton(MongoFieldManager.Default);
        builder.Services.TryAddSingleton(MongoIndexManager.Default);
        builder.Services.TryAddSingleton(MongoQueryManager.Default);

        builder.PreInit(api =>
        {
            api.Fields.Added += factory => CreateFieldOptionsBsonClassMap(factory); //fix for nested field options (inside ArrayFieldOptions)
            api.Fields.Added += factory => MongoFieldManager.Default.EnsureField(factory);
        });

        builder.Init(api =>
        {
            api.MongoFields.AddDefaults();
            api.MongoIndexes.AddDefaults();
            api.MongoQueries.AddDefaults();
        });

        builder.PostInit<AssetIndexInitializer>();
        builder.PostInit<AssetFolderIndexInitializer>();
        builder.PostInit<ContentIndexInitializer>();
        builder.PostInit<SchemaIndexInitializer>();

        ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack() { new IgnoreExtraElementsConvention(true) }, t => true);
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

        return builder;
    }

    private static void CreateFieldOptionsBsonClassMap(FieldFactory fieldFactory)
    {
        if (fieldFactory.OptionsType == null)
        {
            return;
        }

        if (BsonClassMap.IsClassMapRegistered(fieldFactory.OptionsType))
        {
            return;
        }

        BsonClassMap map = new BsonClassMap(fieldFactory.OptionsType);
        map.AutoMap();

        BsonClassMap.RegisterClassMap(map);
    }    
}
