// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.Exports;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.ContentStructures.Queries;
using DragonFly.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.AspNetCore.API.Middlewares.ContentStructures;

static class ContentNodeApiExtensions
{
    public static void MapContentNodeRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("node");

        groupRoute.MapPost("query/{structure:guid}", MapQuery);
        groupRoute.MapPost("", MapCreate);
    }

    private static async Task<QueryResult<RestContentNode>> MapQuery(HttpContext context, IStructureStorage storage, Guid structure)
    {
        var parentIdQuery = context.Request.Query["parentId"];
        Guid? parentId = null;

        if (parentIdQuery.Any() && parentIdQuery.First() is string stringParameter)
        {
            parentId = Guid.Parse(stringParameter);
        }

        QueryResult<ContentNode> items = await storage
                                                .QueryAsync(new NodesQuery() { Structure = structure, ParentId = parentId });

        QueryResult<RestContentNode> restQueryResult = new QueryResult<RestContentNode>();
        restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
        restQueryResult.Offset = items.Offset;
        restQueryResult.Count = items.Count;
        restQueryResult.TotalCount = items.TotalCount;

        return restQueryResult;
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IStructureStorage storage, RestContentNode input)
    {
        ContentNode m = input.ToModel();

        await storage.CreateAsync(m);

        return new ResourceCreated() { Id = m.Id };
    }
}
