using DragonFly.Assets;
using DragonFly.Content;
using DragonFly.Core;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    /// <summary>
    /// ImageAssetProcessing
    /// </summary>
    public class ImageAssetProcessing : IAssetProcessing
    {
        public IEnumerable<string> SupportedMimetypes
        {
            get => new[] { MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp };
        }

        public async Task OnAssetChangedAsync(IAssetProcessingContext context)
        {
            using Stream stream = await context.OpenAssetStreamAsync();

            IImageInfo imageInfo = await Image.IdentifyAsync(stream);

            if (imageInfo != null)
            {
                ImageMetadata imageMetadata = new ImageMetadata() { Width = imageInfo.Width, Height = imageInfo.Height, BitsPerPixel = imageInfo.PixelType.BitsPerPixel };

                await context.AddMetadataAsync(imageMetadata);
            }
        }
    }
}
