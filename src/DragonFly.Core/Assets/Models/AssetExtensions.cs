using DragonFly.Contents.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Core.Assets.Models
{
    public static class AssetExtensions
    {
        public static bool IsImage(this Asset asset)
        {
            if(asset.MimeType == null)
            {
                return false;
            }

            return asset.MimeType.StartsWith("image/");
        }

        public static bool IsSVG(this Asset asset)
        {
            return asset.MimeType == "image/svg+xml";
        }

        public static bool IsPdf(this Asset asset)
        {
            return asset.MimeType == "application/pdf";
        }
    }
}
