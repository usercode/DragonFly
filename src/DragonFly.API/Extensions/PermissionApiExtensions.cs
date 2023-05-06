﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class PermissionApiExtensions
{
    public static void MapPermissionItemApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("permission/query", MapQuery).RequireAuthorization();
    }

    private static async Task MapQuery(HttpContext context, IDragonFlyApi api)
    {
        IEnumerable<Permission> items = api.Permissions().GetAll();

        await context.Response.WriteAsJsonAsync(items);
    }
}
