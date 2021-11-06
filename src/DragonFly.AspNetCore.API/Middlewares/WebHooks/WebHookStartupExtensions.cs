using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Middlewares;
using DragonFly.AspNetCore.API.Middlewares.ContentSchemas;
using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.API.Models.WebHooks;
using DragonFly.AspNetCore.Exports;
using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Core.WebHooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares
{
    static class WebHookStartupExtensions
    {
        public static void MapWebHookRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/webhook/query", MapQuery);
            endpoints.MapGet("api/webhook/{id:guid}", MapGet);
            endpoints.MapPost("api/webhook", MapCreate);
            endpoints.MapPut("api/webhook", MapUpdate);
        }

        private static async Task MapQuery(HttpContext context, JsonService jsonService, IWebHookStorage storage)
        {
            QueryResult<WebHook> items = await storage
                                                     .QueryAsync(new WebHookQuery());

            QueryResult<RestWebHook> restQueryResult = new QueryResult<RestWebHook>();
            restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
            restQueryResult.Offset = items.Offset;
            restQueryResult.Count = items.Count;
            restQueryResult.TotalCount = items.TotalCount;

            string json = jsonService.Serialize(restQueryResult);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapGet(HttpContext context, JsonService jsonService, IWebHookStorage storage, Guid id)
        {
            WebHook result = await storage.GetAsync(id);

            RestWebHook restModel = result.ToRest();

            string json = jsonService.Serialize(restModel);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapCreate(HttpContext context, JsonService jsonService, IWebHookStorage storage)
        {
            RestWebHook input = await jsonService.Deserialize<RestWebHook>(context.Request.Body);

            WebHook model = input.ToModel();

            await storage.CreateAsync(model);

            ResourceCreated result = new ResourceCreated() { Id = model.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapUpdate(HttpContext context, JsonService jsonService, IWebHookStorage storage)
        {
            RestWebHook input = await jsonService.Deserialize<RestWebHook>(context.Request.Body);

            WebHook model = input.ToModel();

            await storage.UpdateAsync(model);

            ResourceCreated result = new ResourceCreated() { Id = model.Id };

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}
