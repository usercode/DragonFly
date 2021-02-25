using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Assets
{
    public class ImageWizardAssetDataUrlService : IImageAssetUrlService
    {
        public string Pdf(Asset asset, int width, int height)
        {
            string url = asset.GetDataUrl();

            if (asset.IsPdf())
            {
                return $"/dragonfly/image/unsafe/pagetoimage(0)/resize({width},{height})/fetch{url}";
            }

            return url;
        }

        public string Resize(Asset asset, int width, int height)
        {
            string url = asset.GetDataUrl();

            if (asset.IsSVG() || asset.IsImage() == false)
            {
                return $"/dragonfly/image/unsafe/fetch{url}";
            }
            else
            {
                return $"/dragonfly/image/unsafe/resize({width},{height})/fetch{url}";
            }            
        }
    }
}
