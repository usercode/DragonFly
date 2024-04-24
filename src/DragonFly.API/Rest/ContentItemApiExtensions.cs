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
using Results;

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

    private static async Task<IResult> MapQuery(IDragonFlyApi api, IContentStorage storage, ContentQuery query)
    {
        return (await storage.QueryAsync(query))
                                .Then(x =>
                                {
                                    foreach (ContentItem contentItem in x.Value.Items)
                                    {
                                        contentItem.ApplySchema();
                                        contentItem.Validate();
                                    }

                                    return Result.Ok(x.Value.Convert(x => x.ToRest()));
                                })
                                .ToHttpResult();
    }

    private static async Task<IResult> MapGet(IDragonFlyApi api, IContentStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.GetContentAsync(schema, id))                                
                                    .Then(x =>
                                    {
                                        if (x.Value is not null)
                                        {
                                            x.Value.ApplySchema();
                                            x.Value.Validate();

                                            return Result.Ok<RestContentItem?>(x.Value.ToRest());
                                        }

                                        return Result.Ok<RestContentItem?>();
                                    })
                                    .ToHttpResult();
    }

    private static async Task<IResult> MapCreate(IDragonFlyApi api, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        var r = await contentStore.CreateAsync(model);

        if (r.IsSucceeded)
        {
            return TypedResults.Ok(new ResourceCreated() { Id = model.Id });
        }
        else
        {
            return r.ToHttpResult();
        }
    }

    private static async Task<IResult> MapUpdate(IDragonFlyApi api, IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        return (await contentStore.UpdateAsync(model)).ToHttpResult();
    }

    private static async Task<IResult> MapDelete(IDragonFlyApi api, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        return (await contentStore.DeleteAsync(content)).ToHttpResult();
    }

    private static async Task<IResult> MapPublish(IDragonFlyApi api, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        return (await contentStore.PublishAsync(content)).ToHttpResult();
    }

    private static async Task<IResult> MapUnpublish(IDragonFlyApi api, IContentStorage contentStore, string schema, Guid id)
    {
        ContentItem? content = await contentStore.GetContentAsync(schema, id);

        if (content == null)
        {
            return TypedResults.NotFound();
        }

        return (await contentStore.UnpublishAsync(content)).ToHttpResult();
    }

    private static async Task<IResult> MapPublishQuery(IDragonFlyApi api, IContentStorage contentStore, ContentQuery query)
    {
        return (await contentStore.PublishQueryAsync(query)).ToHttpResult();
    }

    private static async Task<IResult> MapUnpublishQuery(IDragonFlyApi api, IContentStorage contentStore, ContentQuery query)
    {
        return (await contentStore.UnpublishQueryAsync(query)).ToHttpResult();
    }
}
