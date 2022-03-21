using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Assets.Models;

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
    public IEnumerable<string> UsedMimeTypes { get; set; }
}
