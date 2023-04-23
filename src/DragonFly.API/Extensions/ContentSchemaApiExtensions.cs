// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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

        groupRoute.MapPost("query", MapQuery).RequireAuthorization(SchemaPermissions.SchemaQuery);
        groupRoute.MapGet("{id:guid}", MapGetById).RequireAuthorization(SchemaPermissions.SchemaRead);
        groupRoute.MapGet("{name}", MapGetByName).RequireAuthorization(SchemaPermissions.SchemaRead);
        groupRoute.MapPost("", MapCreate).RequireAuthorization(SchemaPermissions.SchemaCreate);
        groupRoute.MapPut("", MapUpdate).RequireAuthorization(SchemaPermissions.SchemaUpdate);
        groupRoute.MapDelete("{id:guid}", MapDelete).RequireAuthorization(SchemaPermissions.SchemaDelete);
    }

    private static async Task<QueryResult<RestContentSchema>> MapQuery(HttpContext context, ISchemaStorage storage)
    {
        QueryResult<ContentSchema> queryResult = await storage
                                                .QuerySchemasAsync();

        return queryResult.Convert(x => x.ToRest());
    }

    private static async Task<RestContentSchema> MapGetById(HttpContext context, ISchemaStorage storage, Guid id)
    {
        ContentSchema? schema = await storage.GetSchemaAsync(id);

        RestContentSchema restSchema = schema.ToRest();

        return restSchema;
    }

    private static async Task<RestContentSchema> MapGetByName(HttpContext context, ISchemaStorage storage, string name)
    {
        ContentSchema? schema = await storage.GetSchemaAsync(name);

        RestContentSchema restSchema = schema.ToRest();

        return restSchema;
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, ISchemaStorage storage, RestContentSchema input)
    {
        ContentSchema m = input.ToModel();

        await storage.CreateAsync(m);

        var result = new ResourceCreated() { Id = m.Id };

        return result;
    }

    private static async Task MapUpdate(HttpContext context, ISchemaStorage storage, RestContentSchema input)
    {
        ContentSchema m = input.ToModel();

        await storage.UpdateAsync(m);
    }

    private static async Task MapDelete(HttpContext context, ISchemaStorage storage, Guid id)
    {
        ContentSchema? schema = await storage.GetSchemaAsync(id);

        await storage.DeleteAsync(schema);
    }
}
