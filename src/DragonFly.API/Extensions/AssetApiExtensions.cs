// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.AspNet.Middleware;
using DragonFly.Assets.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace DragonFly.AspNetCore.API.Middlewares.Assets;

static class AssetApiExtensions
{
    public static void MapAssetRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("asset");

        groupRoute.MapPost("query", MapQuery);
        groupRoute.MapGet("{id:guid}", MapGet);
        groupRoute.MapPost("", MapCreate);
        groupRoute.MapPut("", MapUpdate);
        groupRoute.MapPost("{id:guid}/publish", MapPublish);
        groupRoute.MapGet("{id:guid}/download", MapDownload);
        groupRoute.MapPost("{id:guid}/upload", MapUpload);
        groupRoute.MapPost("{id:guid}/metadata", MapRefreshMetadata);
    }

    private static async Task<QueryResult<RestAsset>> MapQuery(HttpContext context, IAssetStorage storage, AssetQuery query)
    {
        QueryResult<Asset> assets = await storage.GetAssetsAsync(query);

        QueryResult<RestAsset> queryResult = new QueryResult<RestAsset>();
        queryResult.Offset = assets.Offset;
        queryResult.Count = assets.Count;
        queryResult.TotalCount = assets.TotalCount;
        queryResult.Items = assets.Items.Select(x => x.ToRest()).ToList();

        return queryResult;
    }

    private static async Task<RestAsset> MapGet(HttpContext context, IAssetStorage storage, Guid id)
    {
        Asset entity = await storage.GetAssetAsync(id);

        RestAsset restAsset = entity.ToRest();

        return restAsset;
    }

    private static async Task<ResourceCreated> MapCreate(HttpContext context, IAssetStorage storage, RestAsset restAsset)
    {
        Asset asset = restAsset.ToModel();

        await storage.CreateAsync(asset);

        return new ResourceCreated() { Id = asset.Id };
    }

    private static async Task MapUpdate(HttpContext context, IAssetStorage storage, RestAsset restAsset)
    {
        Asset asset = restAsset.ToModel();

        await storage.UpdateAsync(asset);
    }

    private static async Task MapPublish(HttpContext context, IAssetStorage storage, Guid id)
    {
        await storage.PublishAsync(id);
    }

    private static async Task MapDownload(HttpContext context, IAssetStorage storage, Guid id)
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

    private static async Task MapUpload(HttpContext context, IAssetStorage storage, Guid id)
    {
        await storage.UploadAsync(id, context.Request.ContentType, context.Request.Body);
    }

    private static async Task MapRefreshMetadata(HttpContext context, IAssetStorage storage, Guid id)
    {
        await storage.ApplyMetadataAsync(id);
    }
}
