using DragonFly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Middlewares
{
    class GetApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public GetApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IApiKeyService service)
        {
            if (context.GetRouteValue("id") is string stringId)
            {
                Guid id = Guid.Parse(stringId);

                ApiKey entity = await service.GetApiKey(id);

                await context.Response.WriteAsJsonAsync(entity);
            }
        }
    }
}
