// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using DragonFly.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using SmartResults;

namespace DragonFly.API;

static class AssetApiExtensions
{
    public static void MapAssetRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("asset");

        groupRoute.MapPost("query", MapQuery).Produces<QueryResult<RestAsset>>();
        groupRoute.MapGet("{id:guid}", MapGet).Produces<RestAsset>();
        groupRoute.MapPost("", MapCreate).Produces<ResourceCreated>();
        groupRoute.MapPut("", MapUpdate);
        groupRoute.MapDelete("{id:guid}", MapDelete);
        groupRoute.MapPost("{id:guid}/publish", MapPublish);
        groupRoute.MapGet("{id:guid}/download", MapDownload);
        groupRoute.MapPost("{id:guid}/upload", MapUpload);
        groupRoute.MapPost("{id:guid}/metadata", MapRefreshMetadata);
        groupRoute.MapPost("metadata", MapRefreshMetadataQuery);
    }

    private static async Task<IResult> MapQuery(IAssetStorage storage, AssetQuery query)
    {
        return (await storage.QueryAsync(query))
                             .ToResult(x => x.Convert(x => x.ToRest()))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapGet(IAssetStorage storage, Guid id)
    {
        Asset? entity = await storage.GetAssetAsync(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        RestAsset restAsset = entity.ToRest();

        return TypedResults.Ok(restAsset);
    }

    private static async Task<IResult> MapDelete(IAssetStorage storage, Guid id)
    {
        Asset? entity = await storage.GetAssetAsync(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        return (await storage.DeleteAsync(entity)).ToHttpResult();
    }

    private static async Task<IResult> MapCreate(IAssetStorage storage, RestAsset restAsset)
    {
        Asset asset = restAsset.ToModel();

        return (await storage.CreateAsync(asset))
                             .Then(x => Result.Ok(new ResourceCreated() { Id = asset.Id }))
                             .ToHttpResult();
    }

    private static async Task<IResult> MapUpdate(IAssetStorage storage, RestAsset restAsset)
    {
        Asset asset = restAsset.ToModel();

        return (await storage.UpdateAsync(asset)).ToHttpResult();
    }

    private static async Task<IResult> MapPublish(IAssetStorage storage, Guid id)
    {
        Asset? entity = await storage.GetAssetAsync(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        return (await storage.PublishAsync(entity)).ToHttpResult();
    }

    private static async Task<IResult> MapDownload(HttpContext context, IAssetStorage storage, Guid id)
    {
        Asset? asset = await storage.GetAssetAsync(id);

        if (asset == null)
        {
            return TypedResults.NotFound();
        }

        EntityTagHeaderValue etag = new EntityTagHeaderValue($"\"{asset.Hash}\"");

        if (context.Request.Headers.ETag == etag)
        {
            return TypedResults.StatusCode(StatusCodes.Status304NotModified);
        }

        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue() { Public = true, MaxAge = TimeSpan.FromDays(30) };

        Stream assetStream = await storage.GetStreamAsync(asset);

        return TypedResults.Stream(assetStream, contentType: asset.MimeType, entityTag: etag, enableRangeProcessing: true);
    }

    private static async Task<IResult> MapUpload(HttpContext context, IAssetStorage storage, Guid id)
    {
        if (context.Request.ContentType == null)
        {
            throw new Exception("Content-type was not set.");
        }

        Asset? asset = await storage.GetAssetAsync(id);

        if (asset == null)
        {
            return TypedResults.NotFound();
        }

        return (await storage.UploadAsync(asset, context.Request.ContentType, context.Request.Body)).ToHttpResult();
    }

    private static async Task<IResult> MapRefreshMetadata(IAssetStorage storage, Guid id)
    {
        return await (await storage.GetAssetAsync(id))
                                    .ThenAsync(async x => await storage.ApplyMetadataAsync(x.Value))
                                    .ToHttpResultAsync();
    }

    private static async Task<IResult> MapRefreshMetadataQuery(IAssetStorage storage, AssetQuery query)
    {   
        return (await storage.ApplyMetadataAsync(query)).ToHttpResult();
    }
}
