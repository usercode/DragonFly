// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SmartResults;

namespace DragonFly.API;

static class ContentSchemaApiExtensions
{
    public static void MapContentSchemaRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("schema");

        groupRoute.MapPost("query", MapQuery);
        groupRoute.MapGet("{id:guid}", MapGetById);
        groupRoute.MapGet("{name}", MapGetByName);
        groupRoute.MapPost("", MapCreate);
        groupRoute.MapPut("", MapUpdate);
        groupRoute.MapDelete("{id:guid}", MapDelete);
    }

    private static async Task<IResult> MapQuery(ISchemaStorage storage)
    {
        return (await storage.QuerySchemasAsync())
                             .ToResult(q => q.Convert(i => i.ToRest()))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapGetById(ISchemaStorage storage, Guid id)
    {
        return (await storage.GetSchemaAsync(id))
                             .ToResult(x => x.ToRest())
                             .ToHttpResult();
    }

    private static async Task<IResult> MapGetByName(ISchemaStorage storage, string name)
    {
        return (await storage.GetSchemaAsync(name))
                             .ToResult(x => x.ToRest())
                             .ToHttpResult();
    }

    private static async Task<IResult> MapCreate(ISchemaStorage storage, RestContentSchema input)
    {
        ContentSchema m = input.ToModel();

        return (await storage.CreateAsync(m))
                             .Then(x => Result.Ok(new ResourceCreated() { Id = m.Id }))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapUpdate(ISchemaStorage storage, RestContentSchema input)
    {
        ContentSchema m = input.ToModel();

        return (await storage.UpdateAsync(m))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapDelete(ISchemaStorage storage, Guid id)
    {
        var schema = await storage.GetSchemaAsync(id);
        
        if (schema.IsFailed)
        {
            return schema.ToHttpResult();
        }

        return (await storage.DeleteAsync(schema.Value))
                             .ToHttpResult();
    }
}
