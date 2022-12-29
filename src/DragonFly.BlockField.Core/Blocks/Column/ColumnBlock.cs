// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// ColumnBlock
/// </summary>
public class ColumnBlock : Block, IChildBlocks
{
    public ColumnBlock()
    {
        Columns = new List<Column>();
    }

    public ColumnBlock(params Column[] columns)
        : this()
    {
        Columns = columns;
    }

    public override string CssIcon => "fa-solid fa-table-columns";

    /// <summary>
    /// Columns
    /// </summary>
    public IList<Column> Columns { get; set; }

    /// <summary>
    /// HorizontalAlignment
    /// </summary>
    public HorizontalAlignment? HorizontalAlignment { get; set; }

    public IEnumerable<Block> GetBlocks()
    {
        return Columns.SelectMany(x => x.Blocks);
    }
}
