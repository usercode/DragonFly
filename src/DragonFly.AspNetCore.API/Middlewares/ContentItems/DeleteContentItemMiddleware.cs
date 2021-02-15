using DragonFly.ContentTypes;
using DragonFly.Data;
using DragonFly.Data.Content;
using DragonFly.Data.Models;
using DragonFly.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Middlewares
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
