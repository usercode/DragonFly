// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets.Query;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace DragonFly.API;

static class AssetApiExtensions
{
    public static void MapAssetRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("asset");

        groupRoute.MapPost("query", MapQuery);
        groupRoute.MapGet("{id:guid}", MapGet);
        groupRoute.MapPost("", MapCreate);
        groupRoute.MapPut("", MapUpdate);
        groupRoute.MapDelete("{id:guid}", MapDelete);
        groupRoute.MapPost("{id:guid}/publish", MapPublish);
        groupRoute.MapGet("{id:guid}/download", MapDownload);
        groupRoute.MapPost("{id:guid}/upload", MapUpload);
        groupRoute.MapPost("{id:guid}/metadata", MapRefreshMetadata);
        groupRoute.MapPost("metadata", MapRefreshMetadataQuery);
    }

    private static async Task<QueryResult<RestAsset>> MapQuery(HttpContext context, IAssetStorage storage, AssetQuery query)
    {
        QueryResult<Asset> queryResult = await storage.QueryAsync(query);

        return queryResult.Convert(x => x.ToRest());
    }

    private static async Task<RestAsset> MapGet(HttpContext context, IAssetStorage storage, Guid id)
    {
        Asset entity = await storage.GetRequiredAssetAsync(id);

        RestAsset restAsset = entity.ToRest();

        return restAsset;
    }

    private static async Task MapDelete(HttpContext context, IAssetStorage storage, Guid id)
    {
        Asset entity = await storage.GetRequiredAssetAsync(id);

        await storage.DeleteAsync(entity);
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
        Asset entity = await storage.GetRequiredAssetAsync(id);

        await storage.PublishAsync(entity);
    }

    private static async Task MapDownload(HttpContext context, IAssetStorage storage, Guid id)
    {
        Asset asset = await storage.GetRequiredAssetAsync(id);

        using Stream assetStream = await storage.GetStreamAsync(asset);

        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue() { Public = true, MaxAge = TimeSpan.FromDays(30) };
        context.Response.GetTypedHeaders().ETag = new EntityTagHeaderValue($"\"{asset.Hash}\"");
        context.Response.ContentType = asset.MimeType;
        context.Response.ContentLength = assetStream.Length;

        await assetStream.CopyToAsync(context.Response.Body);
    }

    private static async Task MapUpload(HttpContext context, IAssetStorage storage, Guid id)
    {
        if (context.Request.ContentType == null)
        {
            throw new Exception("Content-type was not set.");
        }

        Asset asset = await storage.GetRequiredAssetAsync(id);

        await storage.UploadAsync(asset, context.Request.ContentType, context.Request.Body);
    }

    private static async Task MapRefreshMetadata(HttpContext context, IAssetStorage storage, Guid id)
    {
        Asset asset = await storage.GetRequiredAssetAsync(id);

        await storage.ApplyMetadataAsync(asset);
    }

    private static async Task<IBackgroundTaskInfo> MapRefreshMetadataQuery(HttpContext context, IAssetStorage storage, AssetQuery query)
    {   
        return await storage.ApplyMetadataAsync(query);
    }
}
