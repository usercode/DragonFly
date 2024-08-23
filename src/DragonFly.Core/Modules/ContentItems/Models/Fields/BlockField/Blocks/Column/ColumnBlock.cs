// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ColumnBlock
/// </summary>
public class ColumnBlock : Block, IChildBlocks
{
    public ColumnBlock()
    {
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
    public IList<Column> Columns { get; set; } = [];

    /// <summary>
    /// HorizontalAlignment
    /// </summary>
    public HorizontalAlignment? HorizontalAlignment { get; set; }

    public IEnumerable<BlockContext> GetBlocks()
    {
        for (int c = 0; c < Columns.Count; c++)
        {
            Column column = Columns[c];

            for (int i = 0; i < column.Blocks.Count; i++)
            {
                int index = i;

                yield return new BlockContext(column.Blocks[i], x => column.Blocks[index] = x);
            }
        }
    }
}
