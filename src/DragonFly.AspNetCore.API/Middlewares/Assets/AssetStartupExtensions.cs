using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.AspNetCore.Exports;
using DragonFly.Content;
using DragonFly.Core.Assets.Queries;
using DragonFly.Storage;
using Microsoft.AspNetCore.Builder;
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
    static class AssetStartupExtensions
    {
        public static void MapAssetRestApi(this IDragonFlyEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/asset/query", MapQuery);
            endpoints.MapGet("api/asset/{id:guid}", MapGet);
            endpoints.MapPost("api/asset", MapCreate);
            endpoints.MapPut("api/asset", MapUpdate);
            endpoints.MapPost("api/asset/{id:guid}/publish", MapPublish);
            endpoints.MapGet("api/asset/{id:guid}/download", MapDownload);
            endpoints.MapPost("api/asset/{id:guid}/upload", MapUpload);
            endpoints.MapPost("api/asset/{id:guid}/metadata", MapRefreshMetadata);
        }

        private static async Task MapQuery(HttpContext context, JsonService jsonService, IAssetStorage storage)
        {
            AssetQuery query = await jsonService.Deserialize<AssetQuery>(context.Request.Body);

            QueryResult<Asset> assets = await storage.GetAssetsAsync(query);

            QueryResult<RestAsset> queryResult = new QueryResult<RestAsset>();
            queryResult.Offset = assets.Offset;
            queryResult.Count = assets.Count;
            queryResult.TotalCount = assets.TotalCount;
            queryResult.Items = assets.Items.Select(x => x.ToRest()).ToList();

            string json = jsonService.Serialize(queryResult);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapGet(HttpContext context, JsonService jsonService, IAssetStorage storage, Guid id)
        {
            Asset entity = await storage.GetAssetAsync(id);

            RestAsset restAsset = entity.ToRest();

            string json = jsonService.Serialize(restAsset);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapCreate(HttpContext context, JsonService jsonService, IAssetStorage storage)
        {
            RestAsset restAsset = await jsonService.Deserialize<RestAsset>(context.Request.Body);

            Asset asset = restAsset.ToModel();

            await storage.CreateAsync(asset);

            var r = new ResourceCreated() { Id = asset.Id };

            string json = jsonService.Serialize(r);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapUpdate(HttpContext context, JsonService jsonService, IAssetStorage storage)
        {
            RestAsset restAsset = await jsonService.Deserialize<RestAsset>(context.Request.Body);

            Asset asset = restAsset.ToModel();

            await storage.UpdateAsync(asset);

            var r = new ResourceCreated() { Id = asset.Id };

            string json = jsonService.Serialize(r);

            await context.Response.WriteAsync(json);
        }

        private static async Task MapPublish(HttpContext context, JsonService jsonService, IAssetStorage storage, Guid id)
        {
            await storage.PublishAsync(id);
        }

        private static async Task MapDownload(HttpContext context, JsonService jsonService, IAssetStorage storage, Guid id)
        {
            Asset asset = await storage.GetAssetAsync(id);

            using (Stream assetStream = await storage.DownloadAsync(id))
            {
                context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue() { Public = true, MaxAge = TimeSpan.FromDays(30) };
                context.Response.GetTypedHeaders().ETag = new EntityTagHeaderValue($"\"{asset.Hash}\"");
                context.Response.ContentType = asset.MimeType;
                context.Response.ContentLength = assetStream.Length;

                await assetStream.CopyToAsync(context.Response.Body);
            }
        }

        private static async Task MapUpload(HttpContext context, JsonService jsonService, IAssetStorage storage, Guid id)
        {
            await storage.UploadAsync(id, context.Request.ContentType, context.Request.Body);
        }

        private static async Task MapRefreshMetadata(HttpContext context, JsonService jsonService, IAssetStorage storage, Guid id)
        {
            await storage.ApplyMetadataAsync(id);
        }
    }
}
