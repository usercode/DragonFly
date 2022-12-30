// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class ContentStructureApiExtensions
{
    public static void MapContentStructureRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("structure");

        groupRoute.MapPost("query", MapQuery);
        groupRoute.MapGet("{id:guid}", MapGetById);
        groupRoute.MapGet("{name}", MapGetByName);
        groupRoute.MapPost("", MapCreate);
        groupRoute.MapPut("", MapUpdate);
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
