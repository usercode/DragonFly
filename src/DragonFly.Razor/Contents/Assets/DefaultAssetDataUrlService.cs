using DragonFly.Contents.Assets;
using DragonFly.Core.Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Assets
{
    class DefaultAssetDataUrlService : IImageAssetUrlService
    {
        public string Pdf(Asset asset)
        {
            string url = asset.GetDataUrl();

            return url;
        }

        public string Resize(Asset asset, int width, int height)
        {
            string url = asset.GetDataUrl();

            return url;
        }
    }
}
