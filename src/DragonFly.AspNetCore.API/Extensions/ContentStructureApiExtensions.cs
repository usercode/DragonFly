using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.Exports;
using DragonFly.Content;
using DragonFly.Builders;
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

static class ContentStructureApiExtensions
{
    public static void MapContentStructureRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/structure/query", MapQuery);
        endpoints.MapGet("api/structure/{id:guid}", MapGetById);
        endpoints.MapGet("api/structure/{name}", MapGetByName);
        endpoints.MapPost("api/structure", MapCreate);
        endpoints.MapPut("api/structure", MapUpdate);
    }

    private static async Task<QueryResult<RestContentStructure>> MapQuery(HttpContext context, IStructureStorage storage, StructureQuery query)
    {
        QueryResult<ContentStructure> items = await storage.QueryAsync(query);

        QueryResult<RestContentStructure> restQueryResult = new QueryResult<RestContentStructure>();
        restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
        restQueryResult.Offset = items.Offset;
        restQueryResult.Count = items.Count;
        restQueryResult.TotalCount = items.TotalCount;

       return restQueryResult;
    }

    private static async Task<RestContentStructure> MapGetById(HttpContext context, IStructureStorage storage, Guid id)
    {
        ContentStructure entity = await storage.GetStructureAsync(id);

        RestContentStructure restSchema = entity.ToRest();

        return restSchema;
    }

    private static async Task<RestContentStructure> MapGetByName(HttpContext context, IStructureStorage storage, string name)
    {
        ContentStructure entity = await storage.GetStructureAsync(name);
        
        RestContentStructure restSchema = entity.ToRest();

       return restSchema;
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IStructureStorage storage, RestContentStructure input)
    {
        ContentStructure m = input.ToModel();

        await storage.CreateAsync(m);

        return new ResourceCreated() { Id = m.Id };
    }

    private static async Task MapUpdate(HttpContext context, IStructureStorage storage, RestContentStructure input)
    {
        ContentStructure m = input.ToModel();

        await storage.UpdateAsync(m);
    }
}
