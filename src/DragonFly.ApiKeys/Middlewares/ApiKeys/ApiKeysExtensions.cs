// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using DragonFLy.ApiKeys.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Middlewares;

static class ApiKeysExtensions
{
    public static void MapApiKeyApi(this IDragonFlyEndpointBuilder endpoints)
    {
        endpoints.MapGet("api/apikey/{id:guid}", MapGet).RequireAuthorization(ApiKeyPermissions.ApiKeyRead);
        endpoints.MapPost("api/apikey/query", MapQuery).RequireAuthorization(ApiKeyPermissions.ApiKeyQuery);
        endpoints.MapPost("api/apikey", MapCreate).RequireAuthorization(ApiKeyPermissions.ApiKeyCreate);
        endpoints.MapPut("api/apikey", MapUpdate).RequireAuthorization(ApiKeyPermissions.ApiKeyUpdate);
        endpoints.MapDelete("api/apikey", MapDelete).RequireAuthorization(ApiKeyPermissions.ApiKeyDelete);
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
