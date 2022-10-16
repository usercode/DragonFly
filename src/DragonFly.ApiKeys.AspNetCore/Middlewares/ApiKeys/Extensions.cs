// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Middlewares;

static class Extensions
{
    public static void MapApiKeyApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("apikey/{id:guid}", MapGet);
        endpoints.MapPost("apikey/query", MapQuery);
        endpoints.MapPost("apikey", MapCreate);
        endpoints.MapPut("apikey", MapUpdate);
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

    private static async Task MapQuery(HttpContext context, IApiKeyService service)
    {
        IEnumerable<ApiKey> items = await service.GetAllApiKeys();

        await context.Response.WriteAsJsonAsync(items);
    }
}
