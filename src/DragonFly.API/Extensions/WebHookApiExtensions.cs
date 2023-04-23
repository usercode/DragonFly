// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class WebHookApiExtensions
{
    public static void MapWebHookRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("webhook");

        groupRoute.MapPost("query", MapQuery).RequireAuthorization(WebHookPermissions.WebHookQuery);
        groupRoute.MapGet("{id:guid}", MapGet).RequireAuthorization(WebHookPermissions.WebHookRead);
        groupRoute.MapPost("", MapCreate).RequireAuthorization(WebHookPermissions.WebHookCreate);
        groupRoute.MapPut("", MapUpdate).RequireAuthorization(WebHookPermissions.WebHookUpdate);
    }

    private static async Task<QueryResult<RestWebHook>> MapQuery(HttpContext context, IWebHookStorage storage)
    {
        QueryResult<WebHook> queryResult = await storage.QueryAsync(new WebHookQuery());

        return queryResult.Convert(x => x.ToRest());
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
