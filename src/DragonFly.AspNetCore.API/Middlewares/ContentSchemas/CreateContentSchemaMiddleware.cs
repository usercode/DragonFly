using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.REST.Models;
using DragonFly.ContentTypes;
using DragonFly.Data;
using DragonFly.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Middlewares.ContentSchemas
{
    class CreateContentSchemaMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateContentSchemaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ISchemaStorage schemaStorage,
            JsonService jsonService)
        {
            string name = (string)context.GetRouteValue("name");

            RestContentSchema input = await jsonService.Deserialize<RestContentSchema>(context.Request.Body);

            ContentSchema m = input.ToModel();

            await schemaStorage.CreateAsync(m);

            var result = new ResourceCreated() { Id = m.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}
