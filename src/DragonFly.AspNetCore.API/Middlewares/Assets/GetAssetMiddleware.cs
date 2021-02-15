using DragonFly.AspNetCore.Rest.Models.Assets;
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

namespace DragonFly.AspNetCore.Rest.Middlewares.Assets
{
    class GetAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public GetAssetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetStorage assetStore,
            JsonService jsonService)
        {
            Guid id = Guid.Parse((string)context.GetRouteValue("id"));

            var entity = await assetStore.GetAssetAsync(id);

            RestAsset restAsset = entity.ToRest();

            string json = jsonService.Serialize(restAsset);

            await context.Response.WriteAsync(json);
        }
    }
}
