using DragonFly.Core;
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
            return asset.MimeType == MimeTypes.Jpeg;
        }

        public static bool IsPng(this Asset asset)
        {
            return asset.MimeType == MimeTypes.Png;
        }

        public static bool IsGif(this Asset asset)
        {
            return asset.MimeType == MimeTypes.Gif;
        }

        public static bool IsBmp(this Asset asset)
        {
            return asset.MimeType == MimeTypes.Bmp;
        }

        public static bool IsSVG(this Asset asset)
        {
            return asset.MimeType == MimeTypes.Svg;
        }

        public static bool IsPdf(this Asset asset)
        {
            return asset.MimeType == MimeTypes.Pdf;
        }

        public static bool IsXml(this Asset asset)
        {
            return asset.MimeType == MimeTypes.Xml;
        }

        public static bool IsPlainText(this Asset asset)
        {
            return asset.MimeType == MimeTypes.PlainText;
        }


    }
}
