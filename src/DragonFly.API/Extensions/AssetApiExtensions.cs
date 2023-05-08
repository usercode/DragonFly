// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Assets.Query;
using DragonFly.Permissions;
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

        groupRoute.MapPost("query", MapQuery).RequirePermission(AssetPermissions.ReadAsset);
        groupRoute.MapGet("{id:guid}", MapGet).RequirePermission(AssetPermissions.ReadAsset);
        groupRoute.MapPost("", MapCreate).RequirePermission(AssetPermissions.CreateAsset);
        groupRoute.MapPut("", MapUpdate).RequirePermission(AssetPermissions.UpdateAsset);
        groupRoute.MapDelete("{id:guid}", MapDelete).RequirePermission(AssetPermissions.DeleteAsset);
        groupRoute.MapPost("{id:guid}/publish", MapPublish).RequirePermission(AssetPermissions.PublishAsset);
        groupRoute.MapGet("{id:guid}/download", MapDownload).RequirePermission(AssetPermissions.DownloadAsset);
        groupRoute.MapPost("{id:guid}/upload", MapUpload).RequirePermission(AssetPermissions.UploadAsset);
        groupRoute.MapPost("{id:guid}/metadata", MapRefreshMetadata).RequirePermission(AssetPermissions.UpdateAsset);
        groupRoute.MapPost("metadata", MapRefreshMetadataQuery).RequirePermission(AssetPermissions.UpdateAsset);
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

    private static async Task<IResult> MapDownload(HttpContext context, IAssetStorage storage, Guid id)
    {
        Asset asset = await storage.GetRequiredAssetAsync(id);

        EntityTagHeaderValue etag = new EntityTagHeaderValue($"\"{asset.Hash}\"");

        if (context.Request.Headers.ETag == etag)
        {
            return Results.StatusCode(StatusCodes.Status304NotModified);
        }

        using Stream assetStream = await storage.GetStreamAsync(asset);

        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue() { Public = true, MaxAge = TimeSpan.FromDays(30) };
        context.Response.GetTypedHeaders().ETag = etag;
        context.Response.ContentType = asset.MimeType;
        context.Response.ContentLength = assetStream.Length;

        await assetStream.CopyToAsync(context.Response.Body);

        return Results.Ok();
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
