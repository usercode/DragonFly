﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetType
/// </summary>
public class AssetType
{
    public static AssetType Image = new AssetType("Image", MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp, MimeTypes.WebP, MimeTypes.Svg);
    public static AssetType Audio = new AssetType("Audio", MimeTypes.Mp3);
    public static AssetType Video = new AssetType("Video", MimeTypes.Mp4, MimeTypes.Ogg, MimeTypes.WebM);
    public static AssetType Document = new AssetType("Document", MimeTypes.Pdf);

    public AssetType(string name, params string[] mimeTypes)
    {
        Name = name;
        UsedMimeTypes = mimeTypes;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// UsedMimeTypes
    /// </summary>
    public string[] UsedMimeTypes { get; set; }
}
