// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Builders;
using DragonFly.ApiKeys.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DragonFly.ApiKeys.AspNetCore.Middlewares;

static class ApiKeysExtensions
{
    public static void MapApiKeyApi(this IDragonFlyEndpointBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("api/apikey");

        group.MapGet("{id:guid}", MapGet).RequirePermission(ApiKeyPermissions.ReadApiKey);
        group.MapPost("query", MapQuery).RequirePermission(ApiKeyPermissions.QueryApiKey);
        group.MapPost("", MapCreate).RequirePermission(ApiKeyPermissions.CreateApiKey);
        group.MapPut("", MapUpdate).RequirePermission(ApiKeyPermissions.UpdateApiKey);
        group.MapDelete("", MapDelete).RequirePermission(ApiKeyPermissions.DeleteApiKey);
    }

    private static async Task<Results<Ok<ApiKey>, NotFound>> MapGet(HttpContext context, IApiKeyService service, Guid id)
    {
        ApiKey? entity = await service.GetApiKey(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(entity);
    }

    private static async Task<Results<Ok, BadRequest>> MapCreate(HttpContext context, IApiKeyService service)
    {
        ApiKey? entity = await context.Request.ReadFromJsonAsync<ApiKey>();

        if (entity == null)
        {
            return TypedResults.BadRequest();
        }

        await service.CreateApiKey(entity);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest>> MapUpdate(HttpContext context, IApiKeyService service)
    {
        ApiKey? entity = await context.Request.ReadFromJsonAsync<ApiKey>();

        if (entity == null)
        {
            return TypedResults.BadRequest();
        }

        await service.UpdateApiKey(entity);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound>> MapDelete(HttpContext context, IApiKeyService service)
    {
        ApiKey? entity = await context.Request.ReadFromJsonAsync<ApiKey>();

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        await service.DeleteApiKey(entity);

        return TypedResults.Ok();
    }

    private static async Task MapQuery(HttpContext context, IApiKeyService service)
    {
        IEnumerable<ApiKey> items = await service.GetAllApiKeys();

        await context.Response.WriteAsJsonAsync(items);
    }
}
