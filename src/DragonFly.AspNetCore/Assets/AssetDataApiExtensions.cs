// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Net.Http.Headers;

namespace DragonFly.AspNetCore;

static class AssetDataApiExtensions
{
    public static void MapAssetDataRestApi(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder groupRoute = endpoints.MapGroup("asset");

        groupRoute.MapGet("{id:guid}/download", MapDownload);
        groupRoute.MapPost("{id:guid}/upload", MapUpload);
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

        Stream assetStream = await storage.OpenStreamAsync(asset.Id);

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

        return (await storage.UploadAsync(asset.Id, context.Request.ContentType, context.Request.Body)).ToHttpResult();
    }
}
