// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SkiaSharp;

namespace DragonFly.AspNetCore;

/// <summary>
/// ImageProcessing
/// </summary>
public class ImageProcessing : IAssetProcessing
{
    public bool CanUse(string mimeType)
    {
        return mimeType switch
        {
            MimeTypes.WebP => true,
            MimeTypes.Jpeg => true,
            MimeTypes.Png => true,
            MimeTypes.Gif => true,
            MimeTypes.Bmp => true,
            _ => false
        };
    }

    public async Task<bool> OnAssetChangedAsync(IAssetProcessingContext context)
    {
        using Stream stream = await context.OpenAssetStreamAsync().ConfigureAwait(false);

        SKImageInfo info = SKBitmap.DecodeBounds(stream);

        ImageMetadata metadata = new ImageMetadata()
        {
            Width = info.Width,
            Height = info.Height,
            BitsPerPixel = info.BitsPerPixel
        };

        await context.SetMetadataAsync(metadata).ConfigureAwait(false);

        return true;
    }
}
