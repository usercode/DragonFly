// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class ContentVersionApiExtensions
{
    public static void MapContentRevisionRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("content");

        groupRoute.MapGet("{schema}/{id:guid}/version", MapGet);
        groupRoute.MapGet("{schema}/{id:guid}/versions", MapQuery);
    }

    private static async Task<Results<Ok<RestContentItem>, NotFound, ForbidHttpResult>> MapGet(IDragonFlyApi api, IContentVersionStorage contentStore, string schema, Guid id)
    {
        if (await api.AuthorizeContentAsync(schema, ContentAction.Read) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        ContentItem? result = await contentStore.GetContentByVersionAsync(schema, id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }

        result.ApplySchema();
        result.Validate();

        RestContentItem restModel = result.ToRest();

        return TypedResults.Ok(restModel);
    }

    private static async Task<Results<Ok<QueryResult<ContentVersionEntry>>, ForbidHttpResult>> MapQuery(IDragonFlyApi api, IContentVersionStorage storage, string schema, Guid id)
    {
        if (await api.AuthorizeContentAsync(schema, ContentAction.Query) == false)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }

        var result = await storage.GetContentVersionsAsync(schema, id);

        QueryResult<ContentVersionEntry> queryResult = new QueryResult<ContentVersionEntry>();
        queryResult.Items = result.ToList();
        queryResult.Offset = 0;
        queryResult.Count = queryResult.Items.Count;
        queryResult.TotalCount = queryResult.Items.Count;

        return TypedResults.Ok(queryResult);
    }

}
