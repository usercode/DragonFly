// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
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
        RouteGroupBuilder group = endpoints.MapGroup("api/apikey");

        group.MapGet("{id:guid}", MapGet).RequirePermission(ApiKeyPermissions.ReadApiKey);
        group.MapPost("query", MapQuery).RequirePermission(ApiKeyPermissions.QueryApiKey);
        group.MapPost("", MapCreate).RequirePermission(ApiKeyPermissions.CreateApiKey);
        group.MapPut("", MapUpdate).RequirePermission(ApiKeyPermissions.UpdateApiKey);
        group.MapDelete("", MapDelete).RequirePermission(ApiKeyPermissions.DeleteApiKey);
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
