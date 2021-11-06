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

namespace DragonFly.AspNetCore.API.Middlewares.ContentSchemas
{
    static class ContentSchemaStartupExtensions
    {
        public static void MapContentSchemaRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/schema/query", MapQuery);
            endpoints.MapGet("api/schema/{id:guid}", MapGetByName);
            endpoints.MapGet("api/schema/{name}", MapGetByName);            
            endpoints.MapPost("api/schema", MapCreate);
            endpoints.MapPut("api/schema", MapUpdate);
        }

        private static async Task MapQuery(HttpContext context, JsonService jsonService, ISchemaStorage storage)
        {
            QueryResult<ContentSchema> items = await storage
                                                    .QuerySchemasAsync();

            QueryResult<RestContentSchema> restQueryResult = new QueryResult<RestContentSchema>();
            restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
            restQueryResult.Offset = items.Offset;
            restQueryResult.Count = items.Count;
            restQueryResult.TotalCount = items.TotalCount;

            string json = jsonService.Serialize(restQueryResult);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapGetByName(HttpContext context, JsonService jsonService, ISchemaStorage storage)
        {
            ContentSchema schema;

            if (context.GetRouteValue("id") is string stringId)
            {
                Guid id = Guid.Parse(stringId);

                schema = await storage.GetSchemaAsync(id);
            }
            else
            {
                string name = (string)context.GetRouteValue("name");

                schema = await storage.GetSchemaAsync(name);
            }

            RestContentSchema restSchema = schema.ToRest();

            string json = jsonService.Serialize(restSchema);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapCreate(HttpContext context, JsonService jsonService, ISchemaStorage storage)
        {
            RestContentSchema input = await jsonService.Deserialize<RestContentSchema>(context.Request.Body);

            ContentSchema m = input.ToModel();

            await storage.CreateAsync(m);

            var result = new ResourceCreated() { Id = m.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapUpdate(HttpContext context, JsonService jsonService, ISchemaStorage storage)
        {
            RestContentSchema input = await jsonService.Deserialize<RestContentSchema>(context.Request.Body);

            ContentSchema m = input.ToModel();

            await storage.UpdateAsync(m);
        }
    }
}
