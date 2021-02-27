using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
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

        public static bool IsJpeg(this Asset asset)
        {
            return asset.MimeType == "image/jpeg";
        }

        public static bool IsPng(this Asset asset)
        {
            return asset.MimeType == "image/png";
        }

        public static bool IsGif(this Asset asset)
        {
            return asset.MimeType == "image/gif";
        }

        public static bool IsBmp(this Asset asset)
        {
            return asset.MimeType == "image/bmp";
        }

        public static bool IsSVG(this Asset asset)
        {
            return asset.MimeType == "image/svg+xml";
        }

        public static bool IsPdf(this Asset asset)
        {
            return asset.MimeType == "application/pdf";
        }

        public static bool IsXml(this Asset asset)
        {
            return asset.MimeType == "application/xml";
        }

        public static bool IsPlainText(this Asset asset)
        {
            return asset.MimeType == "text/plain";
        }


    }
}
