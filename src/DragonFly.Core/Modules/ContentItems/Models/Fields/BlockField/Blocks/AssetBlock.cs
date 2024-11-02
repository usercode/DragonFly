// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetBlock
/// </summary>
public class AssetBlock : Block, IReferencedContent
{
    public AssetBlock()
    {
            
    }

    public AssetBlock(Guid? assetId)
    {
        AssetId = assetId;
    }

    public override string CssIcon => "fa-regular fa-image";

    /// <summary>
    /// AssetId
    /// </summary>
    public Guid? AssetId { get; set; }

    public ContentReference[] GetReferences()
    {
        if (AssetId != null)
        {
            return [new ContentReference(Asset.Schema, AssetId.Value)];
        }

        return [];
    }
}
