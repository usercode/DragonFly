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

namespace DragonFly.AspNetCore.API.Middlewares.AssetFolders
{
    static class AssetFolderStartupExtensions
    {
        public static void MapAssetFolderRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/assetfolder/query", MapQuery);
            endpoints.MapGet("api/assetfolder/{id:guid}", MapGet);
        }

        private static async Task MapQuery(HttpContext context, JsonService jsonService, IAssetFolderStorage storage)
        {
            AssetFolderQuery query = await jsonService.Deserialize<AssetFolderQuery>(context.Request.Body);

            IEnumerable<AssetFolder> assets = await storage.GetAssetFoldersAsync(query);

            IEnumerable<RestAssetFolder> result = assets.Select(x => x.ToRest()).ToList();

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapGet(HttpContext context, JsonService jsonService, IAssetFolderStorage storage, Guid id)
        {
            AssetFolder entity = await storage.GetAssetFolderAsync(id);

            RestAssetFolder restAsset = entity.ToRest();

            string json = jsonService.Serialize(restAsset);

            await context.Response.WriteAsync(json);
        }
    }
}
