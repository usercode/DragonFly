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
    class ExecuteMetadataAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public ExecuteMetadataAssetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetStorage assetStore)
        {
            Guid assetId = Guid.Parse((string)context.GetRouteValue("id"));
            string metadata = (string)context.GetRouteValue("metadata");

            Asset asset = await assetStore.GetAssetAsync(assetId);


        }
    }
}
