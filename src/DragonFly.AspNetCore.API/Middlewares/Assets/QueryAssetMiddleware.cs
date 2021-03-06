﻿using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Core.Assets.Queries;
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
    class QueryAssetMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryAssetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IAssetStorage assetStore,
            JsonService jsonService)
        {
            AssetQuery query = await jsonService.Deserialize<AssetQuery>(context.Request.Body);

            QueryResult<Asset> assets = await assetStore.GetAssetsAsync(query);

            QueryResult<RestAsset> queryResult = new QueryResult<RestAsset>();
            queryResult.Offset = assets.Offset;
            queryResult.Count = assets.Count;
            queryResult.TotalCount = assets.TotalCount;
            queryResult.Items = assets.Items.Select(x => x.ToRest()).ToList();

            string json = jsonService.Serialize(queryResult);

            await context.Response.WriteAsync(json);
        }
    }
}
