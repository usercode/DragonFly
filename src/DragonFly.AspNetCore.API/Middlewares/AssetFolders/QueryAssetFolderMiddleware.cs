using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Contents.Assets;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Assets.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.AssetFolders
{
    class QueryAssetFolderMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryAssetFolderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetFolderStorage assetStore,
            JsonService jsonService)
        {
            AssetFolderQuery query = await jsonService.Deserialize<AssetFolderQuery>(context.Request.Body);

            IEnumerable<AssetFolder> assets = await assetStore.GetAssetFoldersAsync(query);

            IEnumerable<RestAssetFolder> result = assets.Select(x => x.ToRest()).ToList();

            string json = jsonService.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}
