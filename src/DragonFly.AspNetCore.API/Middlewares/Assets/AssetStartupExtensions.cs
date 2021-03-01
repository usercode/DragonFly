using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.Assets
{
    static class AssetStartupExtensions
    {
        public static void MapAssetRestApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapQuery();
            endpoints.MapGet();
            endpoints.MapCreate();
            endpoints.MapUpdate();
            endpoints.MapPublish();
            endpoints.MapDownload();
            endpoints.MapUpload();
        }

        private static IEndpointConventionBuilder MapQuery(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset/query", pipeline);
        }

        private static IEndpointConventionBuilder MapGet(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetAssetMiddleware>()
                                                    .Build();

            return endpoints.MapGet("asset/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapCreate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<CreateAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset", pipeline);
        }

        private static IEndpointConventionBuilder MapUpdate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UpdateAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPut("asset/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapPublish(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<PublishAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset/{id:guid}/publish", pipeline);
        }

        private static IEndpointConventionBuilder MapDownload(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<DownloadAssetMiddleware>()
                                                    .Build();

            return endpoints.MapGet("asset/{id:guid}/download", pipeline);
        }

        private static IEndpointConventionBuilder MapUpload(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UploadAssetMiddleware>()
                                                    .Build();

            return endpoints.MapPost("asset/{id:guid}/upload", pipeline);
        }
    }
}
