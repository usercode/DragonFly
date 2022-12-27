// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class AssetExtensions
{
    public static bool IsImage(this Asset asset)
    {
        return asset.IsWebP() || asset.IsJpeg() || asset.IsPng() || asset.IsGif() || asset.IsBmp();
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

    public static bool IsWebP(this Asset asset)
    {
        return asset.MimeType == MimeTypes.WebP;
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
