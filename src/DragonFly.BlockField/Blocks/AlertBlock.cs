// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// AlertBlock
/// </summary>
public class AlertBlock : Block, IChildBlocks
{
    public AlertBlock()
    {
        AlertType = AlertType.Info;
        Blocks = new List<Block>();
    }

    public override string CssIcon => "fa-solid fa-circle-info";

    public IList<Block> Blocks { get; set; }

    public AlertType AlertType { get; set; }

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
