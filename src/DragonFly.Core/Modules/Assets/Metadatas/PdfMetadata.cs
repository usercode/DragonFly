// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// PdfMetadata
/// </summary>
public class PdfMetadata : AssetMetadata
{
    /// <summary>
    /// CountPages
    /// </summary>
    public int CountPages { get; set; }

    /// <summary>
    /// PdfVersion
    /// </summary>
    public string? PdfVersion { get; set; }

    /// <summary>
    /// IsEncrypted
    /// </summary>
    public bool IsEncrypted { get; set; }
}
