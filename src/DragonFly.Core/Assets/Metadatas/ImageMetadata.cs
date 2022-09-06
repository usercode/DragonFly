using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Assets;

/// <summary>
/// ImageMetadata
/// </summary>
public class ImageMetadata : AssetMetadata
{
    public override string Type => "Image";

    /// <summary>
    /// Width
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Height
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// BitsPerPixel
    /// </summary>
    public int? BitsPerPixel { get; set; }
}
