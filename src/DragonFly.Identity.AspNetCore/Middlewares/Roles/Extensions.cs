using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Middlewares.Roles
{
    internal static class Extensions
    {
        public static void UseRoleApi(this IApplicationBuilder builder)
        {
            builder.Map("/role", x =>
            {
                x.UseRouting();
                x.UseEndpoints(endpoints =>
                {
                    endpoints.MapGetRole();
                    endpoints.MapUpdateRole();
                    endpoints.MapQueryRole();
                });
            });
        }

        private static IEndpointConventionBuilder MapGetRole(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<GetRoleMiddleware>()
                                                    .Build();

            return endpoints.MapGet("{id:guid}", pipeline);
        }

        private static IEndpointConventionBuilder MapUpdateRole(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<UpdateRoleMiddleware>()
                                                    .Build();

            return endpoints.MapPost("", pipeline);
        }

        private static IEndpointConventionBuilder MapQueryRole(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryRoleMiddleware>()
                                                    .Build();

            return endpoints.MapPost("query", pipeline);
        }
    }
}
