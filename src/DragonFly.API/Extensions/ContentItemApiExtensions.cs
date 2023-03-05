// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        QueryResult<ContentItem> queryResult = await storage.QueryAsync(query);

        foreach (ContentItem contentItem in queryResult.Items)
        {
            contentItem.ApplySchema();
            contentItem.Validate();
        }

        return queryResult.Convert(x => x.ToRest());
    }

    private static async Task<RestContentItem> MapGet(IContentStorage contentStore, ISchemaStorage schemaStorage, HttpContext context, string schema, Guid id)
    {
        ContentItem? result = await contentStore.GetContentAsync(schema, id);

        if(result == null)
        {
            throw new Exception($"Content item not found: {schema} / {id}");
        }

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
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            throw new Exception("Content item was not found.");
        }

        await contentStore.DeleteAsync(content);
    }

    private static async Task MapPublish(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            throw new Exception("Content item was not found.");
        }

        await contentStore.PublishAsync(content);
    }

    private static async Task MapUnpublish(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            throw new Exception("Content item was not found.");
        }

        await contentStore.UnpublishAsync(content);
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
