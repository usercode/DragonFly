// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// AssetBlock
/// </summary>
public class AssetBlock : Block
{
    public override string CssIcon => "fa-regular fa-image";

    public Guid? AssetId { get; set; }
}
