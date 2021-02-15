using DragonFly.AspNetCore.Exports;
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
    class CreateContentItemMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateContentItemMiddleware(RequestDelegate next)
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

            await contentStore.CreateAsync(model);

            ResourceCreated result = new ResourceCreated() { Id = model.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}
