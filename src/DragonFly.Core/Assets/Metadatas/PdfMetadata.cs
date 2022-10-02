// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Assets;

/// <summary>
/// PdfMetadata
/// </summary>
public class PdfMetadata : AssetMetadata
{
    /// <summary>
    /// CountPages
    /// </summary>
    public int? CountPages { get; set; }

    /// <summary>
    /// PdfVersion
    /// </summary>
    public string? PdfVersion { get; set; }

    /// <summary>
    /// IsEncrypted
    /// </summary>
    public bool? IsEncrypted { get; set; }
}
