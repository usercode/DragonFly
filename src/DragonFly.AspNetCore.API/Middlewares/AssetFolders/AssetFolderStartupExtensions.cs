using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Middlewares.AssetFolders
{
    static class AssetFolderStartupExtensions
    {
        public static void MapAssetFolderRestApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapQueryAssetFolder();
            endpoints.MapGetAssetFolder();
        }

        private static IEndpointConventionBuilder MapQueryAssetFolder(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryAssetFolderMiddleware>()
                                                    .Build();

            return endpoints.MapPost("assetfolder/query", pipeline);
        }

        private static IEndpointConventionBuilder MapGetAssetFolder(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetAssetFolderMiddleware>()
                                                    .Build();

            return endpoints.MapGet("assetfolder/{id:guid}", pipeline);
        }
    }
}
