// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GridItem
/// </summary>
public class GridItem 
{
    public GridItem()
    {
        ColumnStart = 1;
        ColumnEnd = 2;
        RowStart = 1;
        RowEnd = 2;
    }

    public GridItem(Block block)
        : this()
    {
        Block = block;
    }

    /// <summary>
    /// Blocks
    /// </summary>
    public Block? Block { get; set; }

    public int ColumnStart { get; set; }
    public int ColumnEnd { get; set; }
    public int RowStart { get; set; }
    public int RowEnd { get; set; }
}
