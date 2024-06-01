// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DragonFly.AspNetCore;
using SmartResults;

namespace DragonFly.API;

static class AssetFolderApiExtensions
{
    public static void MapAssetFolderRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("assetfolder");

        groupRoute.MapPost("query", MapQuery).Produces<QueryResult<RestAssetFolder>>();
        groupRoute.MapGet("{id:guid}", MapGet).Produces<RestAssetFolder>();
        groupRoute.MapDelete("{id:guid}", MapDelete);
        groupRoute.MapPost("", MapCreate).Produces<ResourceCreated>();
    }

    private static async Task<IResult> MapQuery(IAssetFolderStorage storage, AssetFolderQuery query)
    {
        return (await storage.QueryAsync(query))
                             .ToResult(x => x.Convert(i => i.ToRest()))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapGet(IAssetFolderStorage storage, Guid id)
    {
        return (await storage.GetAssetFolderAsync(id)).ToHttpResult();
    }

    private static async Task<IResult> MapCreate(IAssetFolderStorage storage, RestAssetFolder input)
    {
        AssetFolder model = input.ToModel();

        return (await storage.CreateAsync(model))
                             .Then(x => Result.Ok(new ResourceCreated() { Id = model.Id }))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapDelete(IAssetFolderStorage storage, Guid id)
    {
        AssetFolder? entity = await storage.GetAssetFolderAsync(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        return (await storage.DeleteAsync(entity)).ToHttpResult();
    }
}
