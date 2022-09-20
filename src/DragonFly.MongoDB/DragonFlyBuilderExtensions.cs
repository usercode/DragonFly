using DragonFly.Builders;
using DragonFly.Core.ContentStructures;
using DragonFly.MongoDB;
using DragonFly.MongoDB.Index;
using DragonFly.MongoDB.Query;
using DragonFly.Storage;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
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
        builder.PreInit(api => api.ContentField().Added += ContentFieldAdded);

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
