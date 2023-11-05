// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class ContentStructureApiExtensions
{
    public static void MapContentStructureRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("structure");

        groupRoute.MapPost("query", MapQuery).RequirePermission();
        groupRoute.MapGet("{id:guid}", MapGetById).RequirePermission();
        groupRoute.MapGet("{name}", MapGetByName).RequirePermission();
        groupRoute.MapPost("", MapCreate).RequirePermission();
        groupRoute.MapPut("", MapUpdate).RequirePermission();
    }

    private static async Task<QueryResult<RestContentStructure>> MapQuery(IStructureStorage storage, StructureQuery query)
    {
        QueryResult<ContentStructure> queryResult = await storage.QueryAsync(query);

        return queryResult.Convert(x => x.ToRest());
    }

    private static async Task<RestContentStructure> MapGetById(IStructureStorage storage, Guid id)
    {
        ContentStructure entity = await storage.GetStructureAsync(id);

        RestContentStructure restSchema = entity.ToRest();

        return restSchema;
    }

    private static async Task<RestContentStructure> MapGetByName(IStructureStorage storage, string name)
    {
        ContentStructure entity = await storage.GetStructureAsync(name);
        
        RestContentStructure restSchema = entity.ToRest();

       return restSchema;
    }

    private static async Task<ResourceCreated> MapCreate(IStructureStorage storage, RestContentStructure input)
    {
        ContentStructure m = input.ToModel();

        await storage.CreateAsync(m);

        return new ResourceCreated() { Id = m.Id };
    }

    private static async Task MapUpdate(IStructureStorage storage, RestContentStructure input)
    {
        ContentStructure m = input.ToModel();

        await storage.UpdateAsync(m);
    }
}
