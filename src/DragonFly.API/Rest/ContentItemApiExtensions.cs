// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Query;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
        groupRoute.MapGet("{schema}/{id:guid}/referencedBy", MapReferencedBy);
        groupRoute.MapPost("{schema}/{id:guid}/publish", MapPublish);
        groupRoute.MapPost("{schema}/{id:guid}/unpublish", MapUnpublish);
        groupRoute.MapPost("publish", MapPublishQuery);
        groupRoute.MapPost("unpublish", MapUnpublishQuery);
        groupRoute.MapPost("rebuildDatabase", MapRebuildDatabase);
    }

    private static async Task<IResult> MapQuery(ISender sender, ContentQuery query)
    {
        return (await sender.Send(query))
                                .ToResult(x => x.Convert(c => c.ToRest()))
                                .ToHttpResult();
    }

    private static async Task<IResult> MapGet(IContentStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.GetContentAsync(schema, id))
                                    .ToResult(x =>
                                    {
                                        if (x is not null)
                                        {
                                            x.ApplySchema();
                                            x.Validate();

                                            return x.ToRest();
                                        }

                                        return null;
                                    })
                                    .ToHttpResult();
    }

    private static async Task<IResult> MapCreate(ISender sender, RestContentItem input)
    {
        ContentItem model = input.ToModel();

        return (await sender.Send(new CreateContentItem { Content = model }))
                                            .Then(x => Result.Ok(new ResourceCreated() { Id = model.Id }))
                                            .ToHttpResult();
    }

    private static async Task<IResult> MapUpdate(ISender sender, RestContentItem input)
    {
        return (await sender.Send(new UpdateContentItem { Content = input.ToModel() })).ToHttpResult();
    }

    private static async Task<IResult> MapDelete(ISender sender, string schema, Guid id)
    {
        return (await sender.Send(new DeleteContentItem { Schema = schema, ContentId = id })).ToHttpResult();
    }

    private static async Task<IResult> MapReferencedBy(IContentStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.GetReferencedByAsync(schema, id)).ToHttpResult();
    }

    private static async Task<IResult> MapPublish(ISender sender, string schema, Guid id)
    {
        return (await sender.Send(new PublishContentItem { Schema = schema, ContentId = id })).ToHttpResult();
    }

    private static async Task<IResult> MapUnpublish(ISender sender, string schema, Guid id)
    {
        return (await sender.Send(new UnpublishContentItem { Schema = schema, ContentId = id })).ToHttpResult();
    }

    private static async Task<IResult> MapPublishQuery(IContentStorage contentStore, ContentQuery query)
    {
        return (await contentStore.PublishQueryAsync(query)).ToHttpResult();
    }

    private static async Task<IResult> MapUnpublishQuery(IContentStorage contentStore, ContentQuery query)
    {
        return (await contentStore.UnpublishQueryAsync(query)).ToHttpResult();
    }

    private static async Task<IResult> MapRebuildDatabase(IContentStorage contentStore)
    {
        return (await contentStore.RebuildDatabaseAsync()).ToHttpResult();
    }
}
