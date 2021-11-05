using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
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
            JsonService jsonService)
        {
            ContentItemQuery query = await jsonService.Deserialize<ContentItemQuery>(context.Request.Body);

            QueryResult<ContentItem> contentItems = await contentStore.QueryAsync(query);

            foreach (ContentItem contentItem in contentItems.Items)
            {
                contentItem.ApplySchema();
            }

            QueryResult<RestContentItem> resultQuery = new QueryResult<RestContentItem>()
            {
                Offset = contentItems.Offset,
                Count = contentItems.Count,
                TotalCount = contentItems.TotalCount,
                Items = contentItems.Items.Select(x => x.ToRest()).ToList()
            };

            string json = jsonService.Serialize(resultQuery);

            await context.Response.WriteAsync(json);
        }
    }
}
