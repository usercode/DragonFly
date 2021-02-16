using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Core;
using DragonFly.Core.Assets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.AssetFolders
{
    class GetAssetFolderMiddleware
    {
        private readonly RequestDelegate _next;

        public GetAssetFolderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetFolderStorage assetStore,
            JsonService jsonService)
        {
            Guid id = Guid.Parse((string)context.GetRouteValue("id"));

            var entity = await assetStore.GetAssetFolderAsync(id);

            RestAssetFolder restAsset = entity.ToRest();

            string json = jsonService.Serialize(restAsset);

            await context.Response.WriteAsync(json);
        }
    }
}
