using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Content;
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
