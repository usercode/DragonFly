using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.AssetFolders
{
    static class AssetFolderStartupExtensions
    {
        public static void UseAssetFolderRestApi(this IApplicationBuilder builder)
        {
            builder.Map("/assetfolder", x =>
            {
                x.UseRouting();
                x.UseEndpoints(endpoints =>
                {
                    endpoints.MapQuery();
                    endpoints.MapGet();
                });
            });
        }

        private static IEndpointConventionBuilder MapQuery(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryAssetFolderMiddleware>()
                                                    .Build();

            return endpoints.MapPost("query", pipeline);
        }

        private static IEndpointConventionBuilder MapGet(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetAssetFolderMiddleware>()
                                                    .Build();

            return endpoints.MapGet("{id:guid}", pipeline);
        }
    }
}
