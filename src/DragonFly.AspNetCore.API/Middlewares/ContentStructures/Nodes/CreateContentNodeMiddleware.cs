using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Core.ContentStructures;

namespace DragonFly.AspNetCore.API.Middlewares.ContentStructures
{
    class CreateContentNodeMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateContentNodeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IStructureStorage storage,
            JsonService jsonService)
        {
            RestContentNode input = await jsonService.Deserialize<RestContentNode>(context.Request.Body);

            ContentNode m = input.ToModel();

            await storage.CreateAsync(m);

            var result = new ResourceCreated() { Id = m.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}
