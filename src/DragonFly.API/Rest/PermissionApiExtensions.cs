// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class PermissionApiExtensions
{
    public static void MapPermissionItemApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("permission/query", MapQuery).RequirePermission();
    }

    private static Ok<IEnumerable<Permission>> MapQuery(IDragonFlyApi api)
    {
        IEnumerable<Permission> items = api.Permission().GetAll();

        return TypedResults.Ok(items);
    }
}
