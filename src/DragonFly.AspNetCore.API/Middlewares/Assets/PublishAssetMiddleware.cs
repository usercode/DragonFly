using DragonFly.Content;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using DragonFly.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Middlewares.Assets
{
    class PublishAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public PublishAssetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            IAssetStorage assetStore)
        {
            Guid id = Guid.Parse((string)context.GetRouteValue("id"));

            await assetStore.PublishAsync(id);
        }
    }
}
