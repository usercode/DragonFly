using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.Exports;
using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.ContentStructures.Queries;
using DragonFly.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.ContentStructures;

static class ContentNodeApiExtensions
{
    public static void MapContentNodeRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/node/query/{structure:guid}", MapQuery);
        endpoints.MapPost("api/node", MapCreate);
    }

    private static async Task<QueryResult<RestContentNode>> MapQuery(HttpContext context, IStructureStorage storage, Guid structure)
    {
        var parentIdQuery = context.Request.Query["parentId"];
        Guid? parentId = null;

        if (parentIdQuery.Any() && string.IsNullOrWhiteSpace(parentIdQuery.First()) == false)
        {
            parentId = Guid.Parse(parentIdQuery.First());
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
