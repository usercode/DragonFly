// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetBlock
/// </summary>
public class AssetBlock : Block
{
    public AssetBlock()
    {
            
    }

    public AssetBlock(Guid? assetId)
    {
        AssetId = assetId;
    }

    public override string CssIcon => "fa-regular fa-image";

    public Guid? AssetId { get; set; }
}
