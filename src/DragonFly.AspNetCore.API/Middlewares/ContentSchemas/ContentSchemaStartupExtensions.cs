﻿using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
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

namespace DragonFly.AspNetCore.API.Middlewares.ContentSchemas
{
    static class ContentSchemaStartupExtensions
    {
        public static void MapContentSchemaRestApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapQuery();
            endpoints.MapGetById();
            endpoints.MapGetByName();            
            endpoints.MapCreate();
            endpoints.MapUpdate();
        }

        private static IEndpointConventionBuilder MapQuery(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapPost("schema/query", pipeline);
        }

        private static IEndpointConventionBuilder MapGetByName(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapGet("schema/{name}", pipeline);
        }

        private static IEndpointConventionBuilder MapGetById(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapGet("schema/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapCreate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<CreateContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapPost("schema", pipeline);
        }

        private static IEndpointConventionBuilder MapUpdate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UpdateContentSchemaMiddleware>()
                                                    .Build();

            return endpoints.MapPut("schema/{id:guid}", pipeline);
        }
    }
}
