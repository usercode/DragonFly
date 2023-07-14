// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
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

        groupRoute.MapPost("query", MapQuery).RequirePermission(ContentPermissions.QueryContent);
        groupRoute.MapGet("{schema}/{id:guid}", MapGet).RequirePermission(ContentPermissions.ReadContent);
        groupRoute.MapPost("", MapCreate).RequirePermission(ContentPermissions.CreateContent);
        groupRoute.MapPut("", MapUpdate).RequirePermission(ContentPermissions.UpdateContent);
        groupRoute.MapDelete("{schema}/{id:guid}", MapDelete).RequirePermission(ContentPermissions.DeleteContent);
        groupRoute.MapPost("{schema}/{id:guid}/publish", MapPublish).RequirePermission(ContentPermissions.PublishContent);
        groupRoute.MapPost("{schema}/{id:guid}/unpublish", MapUnpublish).RequirePermission(ContentPermissions.UnpublishContent);
        groupRoute.MapPost("publish", MapPublishQuery).RequirePermission(ContentPermissions.PublishContent);
        groupRoute.MapPost("unpublish", MapUnpublishQuery).RequirePermission(ContentPermissions.UnpublishContent);
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

    private static async Task<Results<Ok<RestContentItem>, NotFound>> MapGet(IContentStorage contentStore, ISchemaStorage schemaStorage, HttpContext context, string schema, Guid id)
    {
        ContentItem? result = await contentStore.GetContentAsync(schema, id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }

        result.ApplySchema();
        result.Validate();

        RestContentItem restModel = result.ToRest();

        return TypedResults.Ok(restModel);
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        await contentStore.CreateAsync(model);

        return new ResourceCreated() { Id = model.Id };
    }

    private static async Task MapUpdate(HttpContext context, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        await contentStore.UpdateAsync(model);
    }

    private static async Task<Results<Ok, NotFound>> MapDelete(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        await contentStore.DeleteAsync(content);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound>> MapPublish(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        await contentStore.PublishAsync(content);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound>> MapUnpublish(HttpContext context, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        await contentStore.UnpublishAsync(content);

        return TypedResults.Ok();
    }

    private static async Task<IBackgroundTaskInfo> MapPublishQuery(HttpContext context, IContentStorage contentStore, ContentQuery query)
    {
        return await contentStore.PublishQueryAsync(query);
    }

    private static async Task<IBackgroundTaskInfo> MapUnpublishQuery(HttpContext context, IContentStorage contentStore, ContentQuery query)
    {
        return await contentStore.UnpublishQueryAsync(query);
    }
}
