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
        return await storage.QueryAsync(new WebHookQuery())
                            .ThenAsync(x => Result.Ok(x.Value.Convert(i => i.ToRest())))
                            .ToHttpResultAsync();
    }

    private static async Task<RestWebHook?> MapGet(IWebHookStorage storage, Guid id)
    {
        WebHook? result = await storage.GetAsync(id);

        if (result == null)
        {
            return null;
        }

        RestWebHook restModel = result.ToRest();

        return restModel;
    }

    private static async Task<ResourceCreated> MapCreate(IWebHookStorage storage, RestWebHook input)
    {
        WebHook model = input.ToModel();

        await storage.CreateAsync(model);

        return new ResourceCreated() { Id = model.Id };
    }

    private static async Task MapUpdate(IWebHookStorage storage, RestWebHook input)
    {
        WebHook model = input.ToModel();

        await storage.UpdateAsync(model);
    }
}
