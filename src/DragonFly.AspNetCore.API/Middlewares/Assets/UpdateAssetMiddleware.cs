using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Core;
using DragonFly.Core.Assets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.API.Middlewares.Assets
{
    class UpdateAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public UpdateAssetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetStorage assetStore,
            JsonService jsonService)
        {
            RestAsset restAsset = await jsonService.Deserialize<RestAsset>(context.Request.Body);

            Asset asset = restAsset.ToModel();

            await assetStore.UpdateAsync(asset);

            var r = new ResourceCreated() { Id = asset.Id };

            string json = jsonService.Serialize(r);

            await context.Response.WriteAsync(json);
        }
    }
}
