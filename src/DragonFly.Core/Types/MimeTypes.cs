﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// MimeTypes
/// </summary>
public class MimeTypes
{
    public const string Jpeg = "image/jpeg";
    public const string Png = "image/png";
    public const string Gif = "image/gif";
    public const string Bmp = "image/bmp";
    public const string WebP = "image/webp";
    public const string Svg = "image/svg+xml";

    public const string Mp3 = "audio/mpeg";

    public const string Ogg = "video/ogg";
    public const string Mp4 = "video/mp4";
    public const string WebM = "video/webm";

    public const string Pdf = "application/pdf";
    public const string Xml = "application/xml";

    public const string PlainText = "text/plain";

    public static string[] Images = [WebP, Jpeg, Png, Gif, Bmp ];

    public static string[] Videos = [Ogg, Mp4, WebM];

}
