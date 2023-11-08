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
        group.MapDelete("{id:guid}", MapDelete).RequirePermission(ApiKeyPermissions.DeleteApiKey);
    }

    private static async Task<Results<Ok<ApiKey>, NotFound>> MapGet(Guid id, IApiKeyService service)
    {
        ApiKey? entity = await service.GetApiKey(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(entity);
    }

    private static async Task<Ok> MapCreate(ApiKey apiKey1, IApiKeyService service)
    {
        await service.CreateApiKey(apiKey1);

        return TypedResults.Ok();
    }

    private static async Task<Ok> MapUpdate(ApiKey apiKey2, IApiKeyService service)
    {
        await service.UpdateApiKey(apiKey2);

        return TypedResults.Ok();
    }

    private static async Task<Ok> MapDelete(Guid id, IApiKeyService service)
    {
        var apikey = await service.GetApiKey(id);

        await service.DeleteApiKey(apikey);

        return TypedResults.Ok();
    }

    private static async Task<Ok<IEnumerable<ApiKey>>> MapQuery(IApiKeyService service)
    {
        IEnumerable<ApiKey> items = await service.QueryApiKeys();

        return TypedResults.Ok(items);
    }
}
