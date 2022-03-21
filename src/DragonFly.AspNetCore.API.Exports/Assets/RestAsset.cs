using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Models.Assets;

/// <summary>
/// RestAsset
/// </summary>
public class RestAsset : RestContentBase
{
    public RestAsset()
    {
        _metaddata = new Dictionary<string, JsonNode?>();
    }

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

    private IDictionary<string, JsonNode?> _metaddata;

    /// <summary>
    /// Metaddata
    /// </summary>
    public virtual IDictionary<string, JsonNode?> Metaddata { get => _metaddata; set => _metaddata = value; }
}
