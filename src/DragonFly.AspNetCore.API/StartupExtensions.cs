using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.AssetFolders;
using DragonFly.AspNetCore.API.Middlewares.Assets;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
using DragonFly.AspNetCore.API.Middlewares.Logins;
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

namespace DragonFly.AspNetCore.API
{
    public static class StartupExtensions
    {
        public static IDragonFlyBuilder AddRestApi(this IDragonFlyBuilder builder)
        {
            builder.Services.AddSingleton<JsonService>();

            return builder;
        }

        public static IDragonFlyApplicationBuilder UseRestApi(this IDragonFlyApplicationBuilder builder)
        {
            builder.Map("/api", x =>
            {
                x.UseRouting();
                x.UseEndpoints(endpoints => 
                {
                    endpoints.MapContentItemRestApi();
                    endpoints.MapContentSchemaRestApi();
                    endpoints.MapAssetRestApi();
                    endpoints.MapAssetFolderRestApi();
                    endpoints.MapWebHookRestApi();
                });
            });

            return builder;
        }
    }
}
