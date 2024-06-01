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

static class WebHookApiExtensions
{
    public static void MapWebHookRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("webhook");

        groupRoute.MapPost("query", MapQuery).RequirePermission(WebHookPermissions.QueryWebHook);
        groupRoute.MapGet("{id:guid}", MapGet).RequirePermission(WebHookPermissions.ReadWebHook);
        groupRoute.MapPost("", MapCreate).RequirePermission(WebHookPermissions.CreateWebHook);
        groupRoute.MapPut("", MapUpdate).RequirePermission(WebHookPermissions.UpdateWebHook);
    }

    private static async Task<IResult> MapQuery(IWebHookStorage storage)
    {
        return (await storage.QueryAsync(new WebHookQuery()))
                             .ToResult(x => x.Convert(i => i.ToRest()))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapGet(IWebHookStorage storage, Guid id)
    {
        return (await storage.GetAsync(id))
                             .ToResult(x => x.ToRest())
                             .ToHttpResult();
    }

    private static async Task<IResult> MapCreate(IWebHookStorage storage, RestWebHook input)
    {
        WebHook model = input.ToModel();

        return (await storage.CreateAsync(model))
                             .Then(x => Result.Ok(new ResourceCreated() { Id = model.Id }))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapUpdate(IWebHookStorage storage, RestWebHook input)
    {
        WebHook model = input.ToModel();

        return (await storage.UpdateAsync(model))
                             .ToHttpResult();
    }
}
