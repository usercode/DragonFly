using DragonFly.AspNetCore.API.Middlewares;
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

namespace DragonFly.AspNetCore.API.Middlewares
{
    static class WebHookStartupExtensions
    {
        public static void UseWebHookRestApi(this IApplicationBuilder builder)
        {
            builder.Map("/webhook", x =>
            {
                x.UseRouting();
                x.UseEndpoints(endpoints =>
                {
                    endpoints.MapQuery();
                    endpoints.MapGet();
                    endpoints.MapCreate();
                    endpoints.MapUpdate();
                });
            });            
        }

        private static IEndpointConventionBuilder MapQuery(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryWebHookMiddleware>()
                                                    .Build();

            return endpoints.MapPost("query", pipeline);
        }

        private static IEndpointConventionBuilder MapGet(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetWebHookMiddleware>()
                                                    .Build();

            return endpoints.MapGet("{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapCreate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<CreateWebHookMiddleware>()
                                                    .Build();

            return endpoints.MapPost("", pipeline);
        }

        private static IEndpointConventionBuilder MapUpdate(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UpdateWebHookMiddleware>()
                                                    .Build();

            return endpoints.MapPut("{id:guid}", pipeline);
        }
    }
}
