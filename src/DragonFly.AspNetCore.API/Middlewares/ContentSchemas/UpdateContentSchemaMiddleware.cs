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

namespace DragonFly.AspNetCore.API.Middlewares.ContentSchemas
{
    class UpdateContentSchemaMiddleware
    {
        private readonly RequestDelegate _next;

        public UpdateContentSchemaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ISchemaStorage schemaStorage,
            JsonService jsonService)
        {
            Guid id = Guid.Parse((string)context.GetRouteValue("name"));

            RestContentSchema input = await jsonService.Deserialize<RestContentSchema>(context.Request.Body);

            ContentSchema m = input.ToModel();

            await schemaStorage.UpdateAsync(m);
        }
    }
}
