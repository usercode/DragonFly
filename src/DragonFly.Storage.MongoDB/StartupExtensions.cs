using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.MongoDB.Options;
using DragonFly.Storage;
using DragonFly.Storage.MongoDB.Fields;
using DragonFly.Storage.MongoDB.Index;
using DragonFly.Storage.MongoDB.Query;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonFly.AspNetCore;

public static class StartupExtensions
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
