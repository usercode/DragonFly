using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Models;
using DragonFly.AspNetCore.API.Models.WebHooks;
using DragonFly.AspNetCore.Exports;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares;

static class WebHookApiExtensions
{
    public static void MapWebHookRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/webhook/query", MapQuery);
        endpoints.MapGet("api/webhook/{id:guid}", MapGet);
        endpoints.MapPost("api/webhook", MapCreate);
        endpoints.MapPut("api/webhook", MapUpdate);
    }

    private static async Task<QueryResult<RestWebHook>> MapQuery(HttpContext context, IWebHookStorage storage)
    {
        QueryResult<WebHook> items = await storage
                                                 .QueryAsync(new WebHookQuery());

        QueryResult<RestWebHook> restQueryResult = new QueryResult<RestWebHook>();
        restQueryResult.Items = items.Items.Select(x => x.ToRest()).ToList();
        restQueryResult.Offset = items.Offset;
        restQueryResult.Count = items.Count;
        restQueryResult.TotalCount = items.TotalCount;

       return restQueryResult;
    }

    private static async Task<RestWebHook> MapGet(HttpContext context, IWebHookStorage storage, Guid id)
    {
        WebHook result = await storage.GetAsync(id);

        RestWebHook restModel = result.ToRest();

        return restModel;
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IWebHookStorage storage, RestWebHook input)
    {
        WebHook model = input.ToModel();

        await storage.CreateAsync(model);

        return new ResourceCreated() { Id = model.Id };
    }

    private static async Task MapUpdate(HttpContext context, IWebHookStorage storage, RestWebHook input)
    {
        WebHook model = input.ToModel();

        await storage.UpdateAsync(model);
    }
}
