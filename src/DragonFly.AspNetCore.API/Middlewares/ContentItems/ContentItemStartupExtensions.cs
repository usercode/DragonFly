using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Middlewares;
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

namespace DragonFly.AspNetCore.API.Middlewares
{
    static class ContentItemStartupExtensions
    {
        public static void UseContentItemRestApi(this IApplicationBuilder builder)
        {
            builder.Map("/content", x =>
            {
                x.UseRouting();
                x.UseEndpoints(endpoints =>
                {
                    endpoints.MapQuery();
                    endpoints.MapGet();
                    endpoints.MapCreate();
                    endpoints.MapUpdate();
                    endpoints.MapDelete();
                    endpoints.MapPublish();
                });
            });
        }

        private static IEndpointConventionBuilder MapQuery(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryContentItemMiddleware>()
                                                    .Build();

            return endpoints.MapPost("query", pipeline);
        }

        private static IEndpointConventionBuilder MapGet(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetContentItemMiddleware>()                                                 
                                                    .Build();

            return endpoints.MapGet("{schema}/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapCreate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<CreateContentItemMiddleware>()
                                                    .Build();

            return endpoints.MapPost("{schema}", pipeline);
        }

        private static IEndpointConventionBuilder MapUpdate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UpdateContentItemMiddleware>()                                                  
                                                    .Build();

            return endpoints.MapPut("{schema}/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapDelete(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<DeleteContentItemMiddleware>()
                                                    .Build();

            return endpoints.MapDelete("{schema}/{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapPublish(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<PublishContentItemMiddleware>()
                                                    .Build();

            return endpoints.MapPost("{schema}/{id:guid}/publish", pipeline);
        }
    }
}
