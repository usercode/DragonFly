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
    class DownloadAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public DownloadAssetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetStorage assetStore)
        {
            Guid id = Guid.Parse((string)context.GetRouteValue("id"));

            Asset asset = await assetStore.GetAssetAsync(id);

            using (Stream assetStream = await assetStore.DownloadAsync(id))
            {
                context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue() { Public = true, MaxAge = TimeSpan.FromDays(30) };
                context.Response.GetTypedHeaders().ETag = new EntityTagHeaderValue($"\"{asset.Hash}\"");
                context.Response.ContentType = asset.MimeType;
                context.Response.ContentLength = assetStream.Length;

                await assetStream.CopyToAsync(context.Response.Body);
            }
        }
    }
}
