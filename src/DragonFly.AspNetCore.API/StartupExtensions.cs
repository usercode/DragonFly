using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.AssetFolders;
using DragonFly.AspNetCore.API.Middlewares.Assets;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
using DragonFly.Core.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonFly.AspNetCore.API.Middlewares.ContentStructures;
using DragonFly.AspNetCore.Middleware;
using Microsoft.AspNetCore.Http.Json;
using DragonFly.AspNetCore.API.Exports.Json;

namespace DragonFly.AspNetCore
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddRestApi(this IDragonFlyBuilder builder)
        {
            builder.Services.Configure<JsonOptions>(opt =>
            {
                foreach (var converter in JsonSerializerDefault.Options.Converters)
                {
                    opt.SerializerOptions.Converters.Add(converter);
                }
            });

            return builder;
        }

        public static IDragonFlyFullBuilder MapRestApi(this IDragonFlyFullBuilder builder)
        {
            builder.Endpoints(endpoints =>
            {
                endpoints.MapContentItemRestApi();
                endpoints.MapContentSchemaRestApi();
                endpoints.MapContentStructureRestApi();
                endpoints.MapContentNodeRestApi();
                endpoints.MapAssetRestApi();
                endpoints.MapAssetFolderRestApi();
                endpoints.MapWebHookRestApi();
            });

            return builder;
        }
    }
}
