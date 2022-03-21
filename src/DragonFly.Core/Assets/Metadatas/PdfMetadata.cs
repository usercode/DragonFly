﻿using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Assets;

/// <summary>
/// PdfMetadata
/// </summary>
public class PdfMetadata : AssetMetadata
{
    public override string Type => "Pdf";

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
