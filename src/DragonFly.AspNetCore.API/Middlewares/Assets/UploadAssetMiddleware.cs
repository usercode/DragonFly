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
    class UploadAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public UploadAssetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetStorage assetStore)
        {
            Guid id = Guid.Parse((string)context.GetRouteValue("id"));

            await assetStore.UploadAsync(id, context.Request.ContentType, context.Request.Body);
        }
    }
}
