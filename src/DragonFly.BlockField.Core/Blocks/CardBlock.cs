// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// CardBlock
/// </summary>
public class CardBlock : Block
{
    public CardBlock()
    {
        Blocks = new List<Block>();
    }

    public override string CssIcon => "fa-regular fa-id-card";

    /// <summary>
    /// Title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; }

    /// <summary>
    /// Header
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    /// Footer
    /// </summary>
    public string? Footer { get; set; }
}
