// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SmartResults;

namespace DragonFly.API;

static class ContentVersionApiExtensions
{
    public static void MapContentRevisionRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("content");

        groupRoute.MapGet("{schema}/{id:guid}/version", MapGet).Produces<RestContentItem>();
        groupRoute.MapGet("{schema}/{id:guid}/versions", MapQuery).Produces<QueryResult<ContentVersionEntry>>();
    }

    private static async Task<IResult> MapGet(IContentVersionStorage contentStore, string schema, Guid id)
    {
        return (await contentStore.GetContentByVersionAsync(schema, id))
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

    private static async Task<IResult> MapQuery(IContentVersionStorage storage, string schema, Guid id)
    {
        var result = await storage.GetContentVersionsAsync(schema, id);

        QueryResult<ContentVersionEntry> queryResult = new QueryResult<ContentVersionEntry>();
        queryResult.Items = result.Value.ToList();
        queryResult.Offset = 0;
        queryResult.Count = queryResult.Items.Count;
        queryResult.TotalCount = queryResult.Items.Count;

        return TypedResults.Ok(queryResult);
    }

}
