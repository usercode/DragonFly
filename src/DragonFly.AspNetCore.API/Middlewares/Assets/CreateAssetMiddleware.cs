using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Contents.Assets;
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

namespace DragonFly.AspNetCore.API.Middlewares.Assets
{
    class CreateAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateAssetMiddleware(RequestDelegate next)
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

            await assetStore.CreateAsync(asset);

            var r = new ResourceCreated() { Id = asset.Id };

            string json = jsonService.Serialize(r);

            await context.Response.WriteAsync(json);
        }
    }
}
