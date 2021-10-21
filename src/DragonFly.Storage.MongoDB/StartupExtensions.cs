using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Builders;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.WebHooks;
using DragonFly.MongoDB.Options;
using DragonFly.Storage.MongoDB.Fields;
using DragonFly.Storage.MongoDB.Fields.Base;
using DragonFly.Storage.MongoDB.Index;
using DragonFly.Storage.MongoDB.Query;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Data
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddMongoDbStorage(this IDragonFlyBuilder builder)
        {
            return AddMongoDbStorage(builder, x => { });
        }

        public static IDragonFlyBuilder AddMongoDbStorage(this IDragonFlyBuilder builder, Action<MongoDbOptions> options)
        {
            builder.Services.Configure(options);

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
            builder.PreInit(x => x.ContentField().Added += ContentFieldAdded);

            builder.PostInit<CreateIndexAction>();

            return builder;
        }

        private static void AutoMapClass(Type type)
        {
            BsonClassMap map = new BsonClassMap(type);
            map.AutoMap();

            BsonClassMap.RegisterClassMap(map);
        }

        private static void ContentFieldAdded(Type contentFieldType, FieldOptionsAttribute? fieldOptionsAttribute, FieldQueryAttribute? fieldQueryAttribute)
        {
            if (fieldOptionsAttribute != null)
            {
                AutoMapClass(fieldOptionsAttribute.OptionsType);
            }
        }
    }
}
