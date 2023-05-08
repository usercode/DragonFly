// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class ContentNodeApiExtensions
{
    public static void MapContentNodeRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("node");

        groupRoute.MapPost("query/{structure:guid}", MapQuery).RequirePermission();
        groupRoute.MapPost("", MapCreate).RequirePermission();
    }

    private static async Task<QueryResult<RestContentNode>> MapQuery(HttpContext context, IStructureStorage storage, Guid structure)
    {
        var parentIdQuery = context.Request.Query["parentId"];
        Guid? parentId = null;

        if (parentIdQuery.Any() && parentIdQuery.First() is string stringParameter)
        {
            parentId = Guid.Parse(stringParameter);
        }

        QueryResult<ContentNode> queryResult = await storage
                                                .QueryAsync(new NodesQuery() { Structure = structure, ParentId = parentId });

        return queryResult.Convert(x => x.ToRest());
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IStructureStorage storage, RestContentNode input)
    {
        ContentNode m = input.ToModel();

        await storage.CreateAsync(m);

        return new ResourceCreated() { Id = m.Id };
    }
}
