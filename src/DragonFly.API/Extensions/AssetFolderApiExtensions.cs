// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DragonFly.AspNetCore;

namespace DragonFly.API;

static class AssetFolderApiExtensions
{
    public static void MapAssetFolderRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("assetfolder");

        groupRoute.MapPost("query", MapQuery).RequirePermission(AssetFolderPermissions.QueryAssetFolder);
        groupRoute.MapGet("{id:guid}", MapGet).RequirePermission(AssetFolderPermissions.ReadAssetFolder);
        groupRoute.MapDelete("{id:guid}", MapDelete).RequirePermission(AssetFolderPermissions.DeleteAssetFolder);
        groupRoute.MapPost("", MapCreate).RequirePermission(AssetFolderPermissions.CreateAssetFolder);
    }

    private static async Task<QueryResult<RestAssetFolder>> MapQuery(HttpContext context, IAssetFolderStorage storage, AssetFolderQuery query)
    {
        QueryResult<AssetFolder> queryResult = await storage.QueryAsync(query);

        return queryResult.Convert(x => x.ToRest());
    }

    private static async Task<RestAssetFolder> MapGet(HttpContext context, IAssetFolderStorage storage, Guid id)
    {
        AssetFolder? entity = await storage.GetAssetFolderAsync(id);

        if (entity == null)
        {
            throw new Exception("Asset folder not found.");
        }

        return entity.ToRest();
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IAssetFolderStorage storage, RestAssetFolder input)
    {
        AssetFolder model = input.ToModel();

        await storage.CreateAsync(model);

        return new ResourceCreated() { Id = model.Id };
    }

    private static async Task MapDelete(HttpContext context, IAssetFolderStorage storage, Guid id)
    {
        AssetFolder? entity = await storage.GetAssetFolderAsync(id);

        if (entity == null)
        {
            throw new Exception("Asset folder not found.");
        }

        await storage.DeleteAsync(entity);
    }
}
