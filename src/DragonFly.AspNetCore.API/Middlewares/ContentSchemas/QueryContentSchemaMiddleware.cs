using DragonFly.AspNetCore.Rest.Exports;
using DragonFly.AspNetCore.REST.Models;
using DragonFly.ContentTypes;
using DragonFly.Data;
using DragonFly.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Middlewares.ContentSchemas
{
    class QueryContentSchemaMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryContentSchemaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ISchemaStorage schemaStorage,
            JsonService jsonService)
        {
            QueryResult<ContentSchema> items = schemaStorage
                                                    .QueryContentSchemas();

            QueryResult<RestContentSchema> restQueryResult = new QueryResult<RestContentSchema>();
            restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
            restQueryResult.Offset = items.Offset;
            restQueryResult.Count = items.Count;
            restQueryResult.TotalCount = items.TotalCount;

            string json = jsonService.Serialize(restQueryResult);

            await context.Response.WriteAsync(json);
        }
    }
}
