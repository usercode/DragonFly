using DragonFly.Client.Core.Assets;
using DragonFly.Content;
using ImageWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard
{
    /// <summary>
    /// ImageWizardAssetDataUrlService
    /// </summary>
    public class ImageWizardAssetDataUrlService : IAssetPreviewUrlService
    {
        public ImageWizardAssetDataUrlService(ImageUrlBuilder imageUrlBuilder)
        {
            ImageUrlBuilder = imageUrlBuilder;
        }

        /// <summary>
        /// ImageUrlBuilder
        /// </summary>
        private ImageUrlBuilder ImageUrlBuilder { get; }

        public string CreateImageUrl(Asset asset, int width, int height)
        {
            if (asset.IsPdf())
            {
                return ImageUrlBuilder.Asset(asset).PageToImage(0).Resize(width, height).BuildUrl();
            }
            else if (asset.IsSVG())
            {
                return ImageUrlBuilder.Asset(asset).BuildUrl();
            }
            else if (asset.IsImage())
            {
                return ImageUrlBuilder.Asset(asset).Resize(width, height).BuildUrl();
            }            
            else
            {
                return "";
            }
        }
    }
}
