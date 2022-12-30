// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class ContentItemApiExtensions
{
    public static void MapContentItemRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("content");

        groupRoute.MapPost("query", MapQuery);
        groupRoute.MapGet("{schema}/{id:guid}", MapGet);
        groupRoute.MapPost("", MapCreate);
        groupRoute.MapPut("", MapUpdate);
        groupRoute.MapDelete("{schema}/{id:guid}", MapDelete);
        groupRoute.MapPost("{schema}/{id:guid}/publish", MapPublish);
        groupRoute.MapPost("{schema}/{id:guid}/unpublish", MapUnpublish);
        groupRoute.MapPost("publish", MapPublishQuery);
        groupRoute.MapPost("unpublish", MapUnpublishQuery);
    }

    private static async Task<QueryResult<RestContentItem>> MapQuery(HttpContext context, IContentStorage storage, ContentQuery query)
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
        ContentItem? result = await contentStore.GetContentAsync(schema, id);

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

    private static async Task MapPublishQuery(HttpContext context, IContentStorage contentStore, ContentQuery query)
    {
        await contentStore.PublishQueryAsync(query);
    }

    private static async Task MapUnpublishQuery(HttpContext context, IContentStorage contentStore, ContentQuery query)
    {
        await contentStore.UnpublishQueryAsync(query);
    }
}
