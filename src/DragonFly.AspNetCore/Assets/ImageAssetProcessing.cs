// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SixLabors.ImageSharp;

namespace DragonFly.AspNetCore;

/// <summary>
/// ImageAssetProcessing
/// </summary>
public class ImageAssetProcessing : IAssetProcessing
{
    public IEnumerable<string> SupportedMimetypes
    {
        get => new[] { MimeTypes.WebP, MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp };
    }

    public async Task<bool> OnAssetChangedAsync(IAssetProcessingContext context)
    {
        using Stream stream = await context.OpenAssetStreamAsync();

        IImageInfo imageInfo = await Image.IdentifyAsync(stream);

        if (imageInfo != null)
        {
            ImageMetadata imageMetadata = new ImageMetadata() { 
                                                    Width = imageInfo.Width, 
                                                    Height = imageInfo.Height, 
                                                    BitsPerPixel = imageInfo.PixelType.BitsPerPixel };

            await context.SetMetadataAsync(imageMetadata);

            return true;
        }

        return false;
    }
}
