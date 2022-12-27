// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Asset
/// </summary>
public class Asset : ContentBase<Asset>
{
    public Asset()
    {
        //Name = string.Empty;
        //Alt = string.Empty;
        //Description = string.Empty;
        //Slug = string.Empty;
        //MimeType = string.Empty;
        //Hash = string.Empty;
        //PreviewUrl = string.Empty;
        //Size = 0;
        _metaddata = new AssetMetadatas();
    }

    public Asset(Guid id)
       : this()
    {
        Id = id;
    }

    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// Alt
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
    public virtual AssetFolder? Folder { get; set; }

    private AssetMetadatas _metaddata;

    /// <summary>
    /// Metaddata
    /// </summary>
    public virtual AssetMetadatas Metaddata { get => _metaddata; set => _metaddata = value; }
}
