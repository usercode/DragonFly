using DragonFly.AspNetCore.Rest.Middlewares;
using DragonFly.AspNetCore.Rest.Middlewares.ContentSchemas;
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

namespace DragonFly.AspNetCore.Rest.Middlewares.ContentSchemas
{
    static class ContentSchemaStartupExtensions
    {
        public static void MapContentSchemaRestApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapQueryContentItem();
            endpoints.MapGetContentSchema();
            endpoints.MapCreateContentSchema();
            endpoints.MapUpdateContentSchema();
        }

        private static IEndpointConventionBuilder MapQueryContentItem(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapPost("schema/query", pipeline);
        }

        private static IEndpointConventionBuilder MapGetContentSchema(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapGet("schema/{name}", pipeline);
        }

        private static IEndpointConventionBuilder MapCreateContentSchema(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<CreateContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapPost("schema", pipeline);
        }

        private static IEndpointConventionBuilder MapUpdateContentSchema(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UpdateContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapPut("schema/{name}", pipeline);
        }
    }
}
