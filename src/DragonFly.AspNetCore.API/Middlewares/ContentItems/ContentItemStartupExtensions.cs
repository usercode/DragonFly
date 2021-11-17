using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.Core.Builders;
using DragonFly.Data.Models;
using DragonFly.Models;
using DragonFly.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares
{
    static class ContentItemStartupExtensions
    {
        public static void MapContentItemRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/content/query", MapQuery);
            endpoints.MapGet("api/content/{schema}/{id:guid}", MapGet);
            endpoints.MapPost("api/content", MapCreate);
            endpoints.MapPut("api/content", MapUpdate);
            endpoints.MapDelete("api/content/{schema}/{id:guid}", MapDelete);
            endpoints.MapPost("api/content/{schema}/{id:guid}/publish", MapPublish);
            endpoints.MapPost("api/content/publish", MapPublishQuery);
        }

        private static async Task MapQuery(HttpContext context, JsonService jsonService, IContentStorage storage)
        {
            ContentItemQuery query = await jsonService.Deserialize<ContentItemQuery>(context.Request.Body);
            QueryResult<ContentItem> contentItems = await storage.QueryAsync(query);

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

        private static async Task MapGet(IContentStorage contentStore, ISchemaStorage schemaStorage, HttpContext context, JsonService jsonService, string schema, Guid id)
        {
            ContentItem result = await contentStore.GetContentAsync(schema, id);
            ContentSchema schemaModel = await schemaStorage.GetSchemaAsync(schema);

            result.ApplySchema(schemaModel);

            RestContentItem restModel = result.ToRest();

            string json = jsonService.Serialize(restModel);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapCreate(HttpContext context, IContentStorage contentStore, JsonService jsonService)
        {
            RestContentItem input = await jsonService.Deserialize<RestContentItem>(context.Request.Body);

            ContentItem model = input.ToModel();

            await contentStore.CreateAsync(model);

            ResourceCreated result = new ResourceCreated() { Id = model.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapUpdate(HttpContext context, IContentStorage contentStore, JsonService jsonService)
        {
            RestContentItem input = await jsonService.Deserialize<RestContentItem>(context.Request.Body);

            ContentItem model = input.ToModel();

            await contentStore.UpdateAsync(model);

            ResourceCreated result = new ResourceCreated() { Id = input.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapDelete(HttpContext context, IContentStorage contentStore, JsonService jsonService, Guid id)
        {
            string schema = (string)context.GetRouteValue("schema");

            await contentStore.DeleteAsync(schema, id);
        }

        private static async Task MapPublish(HttpContext context, IContentStorage contentStore, JsonService jsonService, Guid id)
        {
            string schema = (string)context.GetRouteValue("schema");

            await contentStore.PublishAsync(schema, id);
        }

        private static async Task MapPublishQuery(HttpContext context, IContentStorage contentStore, JsonService jsonService)
        {
            ContentItemQuery query = await jsonService.Deserialize<ContentItemQuery>(context.Request.Body);

            await contentStore.PublishQueryAsync(query);
        }
    }
}
