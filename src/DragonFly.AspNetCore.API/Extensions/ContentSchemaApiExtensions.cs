using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.Exports;
using DragonFly.Content;
using DragonFly.Core.Builders;
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

namespace DragonFly.AspNetCore.API.Middlewares.ContentSchemas;

static class ContentSchemaApiExtensions
{
    public static void MapContentSchemaRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/schema/query", MapQuery);
        endpoints.MapGet("api/schema/{id:guid}", MapGetById);
        endpoints.MapGet("api/schema/{name}", MapGetByName);            
        endpoints.MapPost("api/schema", MapCreate);
        endpoints.MapPut("api/schema", MapUpdate);
    }

    private static async Task<QueryResult<RestContentSchema>> MapQuery(HttpContext context, ISchemaStorage storage)
    {
        QueryResult<ContentSchema> items = await storage
                                                .QuerySchemasAsync();

        QueryResult<RestContentSchema> restQueryResult = new QueryResult<RestContentSchema>();
        restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
        restQueryResult.Offset = items.Offset;
        restQueryResult.Count = items.Count;
        restQueryResult.TotalCount = items.TotalCount;

        return restQueryResult;
    }

    private static async Task<RestContentSchema> MapGetById(HttpContext context, ISchemaStorage storage, Guid id)
    {
        ContentSchema schema = await storage.GetSchemaAsync(id);

        RestContentSchema restSchema = schema.ToRest();

        return restSchema;
    }

    private static async Task<RestContentSchema> MapGetByName(HttpContext context, ISchemaStorage storage, string name)
    {
        ContentSchema schema = await storage.GetSchemaAsync(name);

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
}
