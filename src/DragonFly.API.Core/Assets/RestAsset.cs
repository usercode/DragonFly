﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;

namespace DragonFly.API;

/// <summary>
/// RestAsset
/// </summary>
public class RestAsset : RestContentBase
{
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public virtual string Alt { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public virtual string Description { get; set; }

    /// <summary>
    /// Filename
    /// </summary>
    public virtual string Slug { get; set; }

    /// <summary>
    /// ContentType
    /// </summary>
    public virtual string MimeType { get; set; }

    /// <summary>
    /// Hash
    /// </summary>
    public virtual string Hash { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    public virtual long Size { get; set; }

    /// <summary>
    /// PreviewUrl
    /// </summary>
    public virtual string PreviewUrl { get; set; }

    /// <summary>
    /// Folder
    /// </summary>
    public virtual RestAssetFolder Folder { get; set; }

    /// <summary>
    /// Metaddata
    /// </summary>
    public virtual JsonObject Metaddata { get; set; } = new JsonObject();
}
