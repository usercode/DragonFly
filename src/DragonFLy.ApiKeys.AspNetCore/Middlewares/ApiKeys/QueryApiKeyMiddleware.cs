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
    class QueryApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IApiKeyService service)
        {
            IEnumerable<ApiKey> items = await service.GetAllApiKeys();

            await context.Response.WriteAsJsonAsync(items);
        }
    }
}
