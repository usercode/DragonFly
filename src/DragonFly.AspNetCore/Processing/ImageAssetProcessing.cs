using DragonFly.Assets;
using DragonFly.Content;
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
        public bool CanUse(Asset asset)
        {
            return asset.IsJpeg() || asset.IsPng() || asset.IsGif() || asset.IsBmp();
        }

        public async Task OnAssetChangedAsync(Asset asset, Stream stream)
        {
            IImageInfo imageInfo = await Image.IdentifyAsync(stream);

            if (imageInfo != null)
            {
                ImageMetadata imageMetadata = new ImageMetadata() { Width = imageInfo.Width, Height = imageInfo.Height };

                asset.SetMetadata(imageMetadata);
            }
        }
    }
}
