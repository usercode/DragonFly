// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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

    private static async Task<QueryResult<RestContentSchema>> MapQuery(ISchemaStorage storage)
    {
        QueryResult<ContentSchema> queryResult = await storage.QuerySchemasAsync();

        return queryResult.Convert(x => x.ToRest());
    }

    private static async Task<RestContentSchema?> MapGetById(ISchemaStorage storage, Guid id)
    {
        ContentSchema? schema = await storage.GetSchemaAsync(id);

        if (schema == null)
        {
            return null;
        }

        RestContentSchema restSchema = schema.ToRest();

        return restSchema;
    }

    private static async Task<RestContentSchema?> MapGetByName(ISchemaStorage storage, string name)
    {
        ContentSchema? schema = await storage.GetSchemaAsync(name);

        if (schema == null)
        {
            return null;
        }

        RestContentSchema restSchema = schema.ToRest();

        return restSchema;
    }

    private static async Task<ResourceCreated> MapCreate(ISchemaStorage storage, RestContentSchema input)
    {
        ContentSchema m = input.ToModel();

        await storage.CreateAsync(m);

        return new ResourceCreated() { Id = m.Id };
    }

    private static async Task MapUpdate(ISchemaStorage storage, RestContentSchema input)
    {
        ContentSchema m = input.ToModel();

        await storage.UpdateAsync(m);
    }

    private static async Task MapDelete(ISchemaStorage storage, Guid id)
    {
        ContentSchema? schema = await storage.GetSchemaAsync(id);
        
        if (schema == null)
        {
            return;
        }

        await storage.DeleteAsync(schema);
    }
}
