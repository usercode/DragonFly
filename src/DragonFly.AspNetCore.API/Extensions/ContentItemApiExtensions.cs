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
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares;

static class ContentItemApiExtensions
{
    public static void MapContentItemRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/content/query", MapQuery);
        endpoints.MapGet("api/content/{schema}/{id:guid}", MapGet);
        endpoints.MapPost("api/content", MapCreate);
        endpoints.MapPut("api/content", MapUpdate);
        endpoints.MapDelete("api/content/{schema}/{id:guid}", MapDelete);
        endpoints.MapPost("api/content/{schema}/{id:guid}/publish", MapPublish);
        endpoints.MapPost("api/content/{schema}/{id:guid}/unpublish", MapUnpublish);
        endpoints.MapPost("api/content/publish", MapPublishQuery);
    }

    private static async Task<QueryResult<RestContentItem>> MapQuery(HttpContext context, IContentStorage storage, ContentItemQuery query)
    {
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

        return resultQuery;
    }

    private static async Task<RestContentItem> MapGet(IContentStorage contentStore, ISchemaStorage schemaStorage, HttpContext context, string schema, Guid id)
    {
        ContentItem result = await contentStore.GetContentAsync(schema, id);

        result.ApplySchema();

        RestContentItem restModel = result.ToRest();

        return restModel;
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        await contentStore.CreateAsync(model);

        ResourceCreated result = new ResourceCreated() { Id = model.Id };

        return result;
    }

    private static async Task MapUpdate(HttpContext context, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        await contentStore.UpdateAsync(model);
    }

    private static async Task MapDelete(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        await contentStore.DeleteAsync(schema, id);
    }

    private static async Task MapPublish(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        await contentStore.PublishAsync(schema, id);
    }

    private static async Task MapUnpublish(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        await contentStore.UnpublishAsync(schema, id);
    }

    private static async Task MapPublishQuery(HttpContext context, IContentStorage contentStore, ContentItemQuery query)
    {
        await contentStore.PublishQueryAsync(query);
    }
}
