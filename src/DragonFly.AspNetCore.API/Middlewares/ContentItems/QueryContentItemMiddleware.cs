using DragonFly.AspNetCore.Rest.Exports;
using DragonFly.ContentTypes;
using DragonFly.Core.Queries;
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
    class QueryContentItemMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryContentItemMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            IContentStorage contentStore, 
            ISchemaStorage schemaStorage,
            JsonService jsonService)
        {
            string schema = (string)context.GetRouteValue("schema");

            QueryParameters queryParameters = await jsonService.Deserialize<QueryParameters>(context.Request.Body);

            ContentSchema schemaModel = await schemaStorage.GetContentSchemaByNameAsync(schema);

            QueryResult<ContentItem> contentItems = await contentStore.Query(schema, queryParameters);

            foreach (ContentItem contentItem in contentItems.Items)
            {
                contentItem.ApplySchema(schemaModel);
            }

            QueryResult<RestContentItem> resultQuery = new QueryResult<RestContentItem>();
            resultQuery.Offset = contentItems.Offset;
            resultQuery.Count = contentItems.Count;
            resultQuery.TotalCount = contentItems.TotalCount;
            resultQuery.Items = contentItems.Items.Select(x => x.ToRest()).ToList();

            string json = jsonService.Serialize(resultQuery);

            await context.Response.WriteAsync(json);
        }
    }
}
