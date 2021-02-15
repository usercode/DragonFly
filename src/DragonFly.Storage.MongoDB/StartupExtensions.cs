using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Builders;
using DragonFly.Core.WebHooks;
using DragonFly.MongoDB.Options;
using Microsoft.Extensions.DependencyInjection;
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
            builder.Services.AddSingleton<IAssetStorage>(x => x.GetRequiredService<MongoStorage>());
            builder.Services.AddSingleton<IAssetFolderStorage>(x => x.GetRequiredService<MongoStorage>());
            builder.Services.AddSingleton<IWebHookStorage>(x => x.GetRequiredService<MongoStorage>());

            return builder;
        }
    }
}
