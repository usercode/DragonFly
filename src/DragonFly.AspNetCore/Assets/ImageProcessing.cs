// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SixLabors.ImageSharp;

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
        using Stream stream = await context.OpenAssetStreamAsync();

        ImageInfo imageInfo = await Image.IdentifyAsync(stream);

        if (imageInfo != null)
        {
            ImageMetadata metadata = new ImageMetadata() { 
                                                    Width = imageInfo.Width, 
                                                    Height = imageInfo.Height, 
                                                    BitsPerPixel = imageInfo.PixelType.BitsPerPixel };

            await context.SetMetadataAsync(metadata);

            return true;
        }

        return false;
    }
}
