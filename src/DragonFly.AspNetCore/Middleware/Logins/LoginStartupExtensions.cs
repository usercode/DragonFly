using DragonFly.AspNetCore.Rest.Middlewares;
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

namespace DragonFly.AspNetCore.Rest.Middlewares.Logins
{
    static class LoginStartupExtensions
    {
        public static void MapDragonFlyLoginRestApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapLogin();
        }

        private static IEndpointConventionBuilder MapLogin(this IEndpointRouteBuilder endpoints)
        {
            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
                                                    .UseMiddleware<LoginMiddleware>()
                                                    .Build();

            return endpoints.MapPost("login", pipeline);
        }

        
    }
}
