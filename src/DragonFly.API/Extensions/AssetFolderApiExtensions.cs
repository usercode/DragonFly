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
        groupRoute.MapDelete("{id:guid}", MapDelete);

        groupRoute.MapPost("", MapCreate);
    }

    private static async Task<IEnumerable<RestAssetFolder>> MapQuery(HttpContext context, IAssetFolderStorage storage, AssetFolderQuery query)
    {
        IEnumerable<AssetFolder> assets = await storage.QueryAsync(query);

        IEnumerable<RestAssetFolder> result = assets.Select(x => x.ToRest()).ToList();

        return result;
    }

    private static async Task<RestAssetFolder> MapGet(HttpContext context, IAssetFolderStorage storage, Guid id)
    {
        AssetFolder entity = await storage.GetAssetFolderAsync(id);

        RestAssetFolder restAsset = entity.ToRest();

       return restAsset;
    }

    private static async Task MapCreate(HttpContext context, IAssetFolderStorage storage, RestAssetFolder input)
    {
        await storage.CreateAsync(input.ToModel());
    }

    private static async Task MapDelete(HttpContext context, IAssetFolderStorage storage, Guid id)
    {
        AssetFolder entity = await storage.GetAssetFolderAsync(id);

        await storage.DeleteAsync(entity);
    }
}
