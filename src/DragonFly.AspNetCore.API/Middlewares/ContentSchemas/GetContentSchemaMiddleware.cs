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

namespace DragonFly.AspNetCore.API.Middlewares.ContentSchemas
{
    class GetContentSchemaMiddleware
    {
        private readonly RequestDelegate _next;

        public GetContentSchemaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ISchemaStorage schemaStorage,
            JsonService jsonService)
        {
            ContentSchema schema;

            if (context.GetRouteValue("id") is string stringId)
            {
                Guid id = Guid.Parse(stringId);

                schema = await schemaStorage.GetSchemaAsync(id);
            }
            else
            {
                string name = (string)context.GetRouteValue("name");

                schema = await schemaStorage.GetSchemaAsync(name);
            }

            RestContentSchema restSchema = schema.ToRest();

            string json = jsonService.Serialize(restSchema);

            await context.Response.WriteAsync(json);
        }
    }
}
