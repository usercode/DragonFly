// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// GridBlock
/// </summary>
public class GridBlock : Block, IChildBlocks
{
    public GridBlock()
    {
        Columns = new List<GridSpan>();
        Rows = new List<GridSpan>();
        Items = new List<GridItem>();
    }

    public GridBlock(params GridSpan[] columns)
        : this()
    {
        Columns = columns;
    }

    public override string CssIcon => "fa-solid fa-grip";

    /// <summary>
    /// Columns
    /// </summary>
    public IList<GridSpan> Columns { get; set; }

    /// <summary>
    /// Rows
    /// </summary>
    public IList<GridSpan> Rows { get; set; }

    /// <summary>
    /// Items
    /// </summary>
    public IList<GridItem> Items { get; set; }

    public IEnumerable<Block> GetBlocks() => Items.Select(x => x.Block);
}
