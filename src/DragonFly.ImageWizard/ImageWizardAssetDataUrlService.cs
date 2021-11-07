using DragonFly.Client.Core.Assets;
using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard
{
    public class ImageWizardAssetDataUrlService : IAssetPreviewUrlService
    {
        public string GetImageUrl(Asset asset, int width, int height)
        {
            string assetId = $"{asset.Id}?v={asset.Hash}";

            if (asset.IsPdf())
            {
                return $"/dragonfly/image/unsafe/pagetoimage(0)/resize({width},{height})/dragonfly/{assetId}";
            }
            else if (asset.IsSVG())
            {
                return $"/dragonfly/image/unsafe/dragonfly/{assetId}";
            }
            else if (asset.IsImage())
            {
                return $"/dragonfly/image/unsafe/resize({width},{height})/dragonfly/{assetId}";
            }            
            else
            {
                return "";
            }
        }
    }
}
