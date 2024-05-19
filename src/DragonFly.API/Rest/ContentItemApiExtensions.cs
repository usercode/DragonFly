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
using SmartResults;

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

    private static async Task<IResult> MapQuery(IContentStorage storage, ContentQuery query)
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

    private static async Task<IResult> MapGet(IContentStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.GetContentAsync(new ContentId(schema, id)))
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

    private static async Task<IResult> MapCreate(IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        var result = await contentStore.CreateAsync(model);

        return result.Match(() => TypedResults.Ok(new ResourceCreated() { Id = model.Id }), x => result.ToHttpResult());
    }

    private static async Task<IResult> MapUpdate(IContentStorage contentStore, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        return (await contentStore.UpdateAsync(model)).ToHttpResult();
    }

    private static async Task<IResult> MapDelete(IContentStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.DeleteAsync(new ContentId(schema, id))).ToHttpResult();
    }

    private static async Task<IResult> MapPublish(IContentStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.PublishAsync(new ContentId(schema, id))).ToHttpResult();
    }

    private static async Task<IResult> MapUnpublish(IContentStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.UnpublishAsync(new ContentId(schema, id))).ToHttpResult();
    }

    private static async Task<IResult> MapPublishQuery(IContentStorage contentStore, ContentQuery query)
    {
        return (await contentStore.PublishQueryAsync(query)).ToHttpResult();
    }

    private static async Task<IResult> MapUnpublishQuery(IContentStorage contentStore, ContentQuery query)
    {
        return (await contentStore.UnpublishQueryAsync(query)).ToHttpResult();
    }
}
