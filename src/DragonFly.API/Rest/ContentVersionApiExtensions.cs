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

    private static async Task<IResult> MapQuery(IContentVersionStorage storage, string schema, Guid id)
    {
        return (await storage.GetContentVersionsAsync(schema, id))
                                .ToHttpResult();
    }
}
