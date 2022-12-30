// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DragonFly.API;

static class AssetFolderApiExtensions
{
    public static void MapAssetFolderRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("assetfolder");

        groupRoute.MapPost("query", MapQuery);
        groupRoute.MapGet("{id:guid}", MapGet);
    }

    private static async Task<IEnumerable<RestAssetFolder>> MapQuery(HttpContext context, IAssetFolderStorage storage, AssetFolderQuery query)
    {
        IEnumerable<AssetFolder> assets = await storage.GetAssetFoldersAsync(query);

        IEnumerable<RestAssetFolder> result = assets.Select(x => x.ToRest()).ToList();

        return result;
    }

    private static async Task<RestAssetFolder> MapGet(HttpContext context, IAssetFolderStorage storage, Guid id)
    {
        AssetFolder entity = await storage.GetAssetFolderAsync(id);

        RestAssetFolder restAsset = entity.ToRest();

       return restAsset;
    }
}
