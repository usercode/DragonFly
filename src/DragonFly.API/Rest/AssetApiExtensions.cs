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
using Results;

namespace DragonFly.API;

static class AssetApiExtensions
{
    public static void MapAssetRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("asset");

        groupRoute.MapPost("query", MapQuery).Produces<QueryResult<RestAsset>>();
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

    private static async Task<IResult> MapQuery(IAssetStorage storage, AssetQuery query)
    {
        return (await storage.QueryAsync(query))
                        .Then(x => Result.Ok(x.Value.Convert(x => x.ToRest())))
                        .ToHttpResult();
    }

    private static async Task<Results<Ok<RestAsset>, NotFound>> MapGet(IAssetStorage storage, Guid id)
    {
        Asset? entity = await storage.GetAssetAsync(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        RestAsset restAsset = entity.ToRest();

        return TypedResults.Ok(restAsset);
    }

    private static async Task<Results<Ok, NotFound>> MapDelete(IAssetStorage storage, Guid id)
    {
        Asset? entity = await storage.GetAssetAsync(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        await storage.DeleteAsync(entity);

        return TypedResults.Ok();
    }

    private static async Task<ResourceCreated> MapCreate(IAssetStorage storage, RestAsset restAsset)
    {
        Asset asset = restAsset.ToModel();

        await storage.CreateAsync(asset);

        return new ResourceCreated() { Id = asset.Id };
    }

    private static async Task MapUpdate(IAssetStorage storage, RestAsset restAsset)
    {
        Asset asset = restAsset.ToModel();

        await storage.UpdateAsync(asset);
    }

    private static async Task<Results<Ok, NotFound>> MapPublish(IAssetStorage storage, Guid id)
    {
        Asset? entity = await storage.GetAssetAsync(id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        await storage.PublishAsync(entity);

        return TypedResults.Ok();
    }

    private static async Task<Results<FileStreamHttpResult, NotFound, StatusCodeHttpResult>> MapDownload(HttpContext context, IAssetStorage storage, Guid id)
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
        return await storage
                            .GetAssetAsync(id)
                            .ThenAsync(x => storage.ApplyMetadataAsync(x.Value))
                            .ToHttpResultAsync();
    }

    private static async Task<IResult> MapRefreshMetadataQuery(IAssetStorage storage, AssetQuery query)
    {   
        return (await storage.ApplyMetadataAsync(query)).ToHttpResult();
    }
}
