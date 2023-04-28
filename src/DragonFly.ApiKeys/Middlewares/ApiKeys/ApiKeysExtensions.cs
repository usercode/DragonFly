// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFLy.ApiKeys.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFLy.ApiKeys.AspNetCore.Middlewares;

static class ApiKeysExtensions
{
    public static void MapApiKeyApi(this IDragonFlyEndpointBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("api/apikey").RequireAuthorization();

        group.MapGet("{id:guid}", MapGet).RequireAuthorization(ApiKeyPermissions.ApiKeyRead);
        group.MapPost("query", MapQuery).RequireAuthorization(ApiKeyPermissions.ApiKeyQuery);
        group.MapPost("", MapCreate).RequireAuthorization(ApiKeyPermissions.ApiKeyCreate);
        group.MapPut("", MapUpdate).RequireAuthorization(ApiKeyPermissions.ApiKeyUpdate);
        group.MapDelete("", MapDelete).RequireAuthorization(ApiKeyPermissions.ApiKeyDelete);
    }

    private static async Task MapGet(HttpContext context, IApiKeyService service, Guid id)
    {
        ApiKey entity = await service.GetApiKey(id);

        await context.Response.WriteAsJsonAsync(entity);
    }

    private static async Task MapCreate(HttpContext context, IApiKeyService service)
    {
        ApiKey? entity = await context.Request.ReadFromJsonAsync<ApiKey>();

        await service.CreateApiKey(entity);
    }

    private static async Task MapUpdate(HttpContext context, IApiKeyService service)
    {
        ApiKey? entity = await context.Request.ReadFromJsonAsync<ApiKey>();

        await service.UpdateApiKey(entity);
    }

    private static async Task MapDelete(HttpContext context, IApiKeyService service)
    {
        ApiKey? entity = await context.Request.ReadFromJsonAsync<ApiKey>();

        await service.DeleteApiKey(entity);
    }

    private static async Task MapQuery(HttpContext context, IApiKeyService service)
    {
        IEnumerable<ApiKey> items = await service.GetAllApiKeys();

        await context.Response.WriteAsJsonAsync(items);
    }
}
