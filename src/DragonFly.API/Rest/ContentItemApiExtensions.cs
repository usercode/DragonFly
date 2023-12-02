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

    private static async Task<Results<Ok<QueryResult<RestContentItem>>, ForbidHttpResult>> MapQuery(IDragonFlyApi api, IContentStorage storage, ContentQuery query)
    {
        if (await api.AuthorizeContentAsync(query.Schema, ContentAction.Query) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        QueryResult<ContentItem> queryResult = await storage.QueryAsync(query);

        foreach (ContentItem contentItem in queryResult.Items)
        {
            contentItem.ApplySchema();
            contentItem.Validate();
        }

        return TypedResults.Ok(queryResult.Convert(x => x.ToRest()));
    }

    private static async Task<Results<Ok<RestContentItem>, NotFound, ForbidHttpResult>> MapGet(IDragonFlyApi api, IContentStorage contentStore, ISchemaStorage schemaStorage, HttpContext context, string schema, Guid id)
    {
        if (await api.AuthorizeContentAsync(schema, ContentAction.Read) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

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

    private static async Task<Results<Ok<ResourceCreated>, ForbidHttpResult>> MapCreate(IDragonFlyApi api, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        if (await api.AuthorizeContentAsync(model.Schema.Name, ContentAction.Create) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        await contentStore.CreateAsync(model);

        return TypedResults.Ok(new ResourceCreated() { Id = model.Id });
    }

    private static async Task<Results<Ok, ForbidHttpResult>> MapUpdate(IDragonFlyApi api, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        if (await api.AuthorizeContentAsync(model.Schema.Name, ContentAction.Update) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        await contentStore.UpdateAsync(model);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound, ForbidHttpResult>> MapDelete(IDragonFlyApi api, IContentStorage contentStore, string schema, Guid id)
    {
        if (await api.AuthorizeContentAsync(schema, ContentAction.Delete) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        await contentStore.DeleteAsync(content);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound, ForbidHttpResult>> MapPublish(IDragonFlyApi api, IContentStorage contentStore, string schema, Guid id)
    {
        if (await api.AuthorizeContentAsync(schema, ContentAction.Publish) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        await contentStore.PublishAsync(content);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound, ForbidHttpResult>> MapUnpublish(IDragonFlyApi api, IContentStorage contentStore, string schema, Guid id)
    {
        if (await api.AuthorizeContentAsync(schema, ContentAction.Unpublish) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        await contentStore.UnpublishAsync(content);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok<IBackgroundTaskInfo>, ForbidHttpResult>> MapPublishQuery(IDragonFlyApi api, IContentStorage contentStore, ContentQuery query)
    {
        if (await api.AuthorizeContentAsync(query.Schema, ContentAction.Publish) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        IBackgroundTaskInfo result = await contentStore.PublishQueryAsync(query);

        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IBackgroundTaskInfo>, ForbidHttpResult>> MapUnpublishQuery(IDragonFlyApi api, IContentStorage contentStore, ContentQuery query)
    {
        if (await api.AuthorizeContentAsync(query.Schema, ContentAction.Unpublish) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        IBackgroundTaskInfo result =  await contentStore.UnpublishQueryAsync(query);

        return TypedResults.Ok(result);
    }
}
