// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GridBlock
/// </summary>
public class GridBlock : Block, IChildBlocks
{
    public GridBlock()
    {
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
    public IList<GridSpan> Columns { get; set; } = [];

    /// <summary>
    /// Rows
    /// </summary>
    public IList<GridSpan> Rows { get; set; } = [];

    /// <summary>
    /// Items
    /// </summary>
    public IList<GridItem> Items { get; set; } = [];

    public IEnumerable<BlockContext> GetBlocks()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            GridItem column = Items[i];

            yield return new BlockContext(column.Block, x => column.Block = x);
        }
    }
}
