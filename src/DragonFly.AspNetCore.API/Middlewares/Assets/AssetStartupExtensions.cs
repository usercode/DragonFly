using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Middlewares.Assets
{
    static class AssetStartupExtensions
    {
        public static void MapAssetRestApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapQueryAsset();
            endpoints.MapGetAsset();
            endpoints.MapCreateAsset();
            endpoints.MapUpdateAsset();
            endpoints.MapPublishAsset();
            endpoints.MapDownloadAsset();
            endpoints.MapUploadAsset();
        }

        private static IEndpointConventionBuilder MapQueryAsset(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset/query", pipeline);
        }

        private static IEndpointConventionBuilder MapGetAsset(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetAssetMiddleware>()
                                                    .Build();

            return endpoints.MapGet("asset/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapCreateAsset(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<CreateAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset", pipeline);
        }

        private static IEndpointConventionBuilder MapUpdateAsset(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UpdateAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPut("asset/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapPublishAsset(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<PublishAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset/{id:guid}/publish", pipeline);
        }

        private static IEndpointConventionBuilder MapDownloadAsset(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<DownloadAssetMiddleware>()
                                                    .Build();

            return endpoints.MapGet("asset/{id:guid}/download", pipeline);
        }

        private static IEndpointConventionBuilder MapUploadAsset(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UploadAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset/{id:guid}/upload", pipeline);
        }
    }
}
