using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore
{
    internal static class Extensions
    {
        public static void UsePermissionItemApi(this IApplicationBuilder builder)
        {
            builder.Map("/permission", x =>
            {
                x.UseRouting();
                x.UseEndpoints(endpoints =>
                {
                    endpoints.MapQueryPermission();
                });
            });
        }

        private static IEndpointConventionBuilder MapQueryPermission(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<QueryPermissionMiddleware>()
                                                    .Build();

            return endpoints.MapPost("query", pipeline);
        }
    }
}
