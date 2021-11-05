using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Middlewares
{
    internal static class Extensions
    {
        public static void UseApiKeyApi(this IApplicationBuilder builder)
        {
            builder.Map("/api/apikey", x =>
            {
                x.UseRouting();
                x.UseEndpoints(endpoints =>
                {
                    endpoints.MapGetApiKey();
                    endpoints.MapQueryApiKey();
                });
            });
        }

        private static IEndpointConventionBuilder MapGetApiKey(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetApiKeyMiddleware>()
                                                    .Build();

            return endpoints.MapGet("{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapQueryApiKey(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryApiKeyMiddleware>()
                                                    .Build();

            return endpoints.MapPost("query", pipeline);
        }
    }
}
