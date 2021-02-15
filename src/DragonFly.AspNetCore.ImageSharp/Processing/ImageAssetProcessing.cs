using DragonFly.Contents.Assets;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Assets
{
    public class ImageAssetProcessing : IAssetProcessing
    {
        public IEnumerable<string> MimeTypes => new [] { "image/png", "image/jpeg", "image/gif" };

        public async Task OnAssetChangedAsync(Asset asset, Stream stream)
        {
            IImageInfo imageInfo = await Image.IdentifyAsync(stream);

            if (imageInfo != null)
            {
                ImageMetadata imageMetadata = new ImageMetadata() { Width = imageInfo.Width, Height = imageInfo.Height };

                asset.Metaddata.TryAdd("Image", imageMetadata);
            }
        }
    }
}
