using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
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

namespace DragonFly.AspNetCore.API.Middlewares.ContentStructures
{
    static class ContentStructureStartupExtensions
    {
        public static void MapContentStructureRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/structure/query", MapQuery);
            endpoints.MapGet("api/structure/{id:guid}", MapGetByName);
            endpoints.MapGet("api/structure/{name}", MapGetByName);
            endpoints.MapPost("api/structure", MapCreate);
            endpoints.MapPut("api/structure", MapUpdate);
        }

        private static async Task MapQuery(HttpContext context, JsonService jsonService, IStructureStorage storage)
        {
            StructureQuery query = await jsonService.Deserialize<StructureQuery>(context.Request.Body);

            QueryResult<ContentStructure> items = await storage.QueryAsync(query);

            QueryResult<RestContentStructure> restQueryResult = new QueryResult<RestContentStructure>();
            restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
            restQueryResult.Offset = items.Offset;
            restQueryResult.Count = items.Count;
            restQueryResult.TotalCount = items.TotalCount;

            string json = jsonService.Serialize(restQueryResult);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapGetByName(HttpContext context, JsonService jsonService, IStructureStorage storage)
        {
            ContentStructure entity;

            if (context.GetRouteValue("id") is string stringId)
            {
                Guid id = Guid.Parse(stringId);

                entity = await storage.GetStructureAsync(id);
            }
            else
            {
                string name = (string)context.GetRouteValue("name");

                entity = await storage.GetStructureAsync(name);
            }

            RestContentStructure restSchema = entity.ToRest();

            string json = jsonService.Serialize(restSchema);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapCreate(HttpContext context, JsonService jsonService, IStructureStorage storage)
        {
            RestContentStructure input = await jsonService.Deserialize<RestContentStructure>(context.Request.Body);

            ContentStructure m = input.ToModel();

            await storage.CreateAsync(m);

            var result = new ResourceCreated() { Id = m.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapUpdate(HttpContext context, JsonService jsonService, IStructureStorage storage)
        {
            RestContentStructure input = await jsonService.Deserialize<RestContentStructure>(context.Request.Body);

            ContentStructure m = input.ToModel();

            await storage.UpdateAsync(m);
        }
    }
}
