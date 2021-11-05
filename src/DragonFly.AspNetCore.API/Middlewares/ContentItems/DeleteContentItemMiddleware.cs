using DragonFly.Content;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using DragonFly.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares
{
    class DeleteContentItemMiddleware
    {
        private readonly RequestDelegate _next;

        public DeleteContentItemMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            IContentStorage contentStore)
        {
            Guid contentId = Guid.Parse((string)context.GetRouteValue("id"));
            string schema = (string)context.GetRouteValue("schema");

            await contentStore.DeleteAsync(schema, contentId);
        }
    }
}
