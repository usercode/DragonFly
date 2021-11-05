using DragonFly.AspNetCore.Exports;
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
    class UpdateContentItemMiddleware
    {
        private readonly RequestDelegate _next;

        public UpdateContentItemMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            IContentStorage contentStore,
            JsonService jsonService)
        {
            RestContentItem input = await jsonService.Deserialize<RestContentItem>(context.Request.Body);

            ContentItem model = input.ToModel();

            await contentStore.UpdateAsync(model);

            ResourceCreated result = new ResourceCreated() { Id = input.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}
