using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Content;
using DragonFly.Core.Assets.Queries;
using DragonFly.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.AssetFolders;

static class AssetFolderApiExtensions
{
    public static void MapAssetFolderRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/assetfolder/query", MapQuery);
        endpoints.MapGet("api/assetfolder/{id:guid}", MapGet);
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
