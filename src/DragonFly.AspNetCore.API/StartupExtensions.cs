using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.AssetFolders;
using DragonFly.AspNetCore.API.Middlewares.Assets;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
using DragonFly.AspNet.Middleware;
using DragonFly.Core.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API.Middlewares.ContentStructures;
using DragonFly.AspNetCore.API;
using DragonFly.AspNetCore.Middleware;

namespace DragonFly.AspNetCore
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddRestApi(this IDragonFlyBuilder builder)
        {
            builder.Services.AddSingleton<JsonService>();

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
