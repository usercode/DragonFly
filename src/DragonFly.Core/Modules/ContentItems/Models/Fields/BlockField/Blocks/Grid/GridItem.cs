// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GridItem
/// </summary>
public class GridItem 
{
    /// <summary>
    /// Blocks
    /// </summary>
    public required Block Block { get; set; }

    public int ColumnStart { get; set; } = 1;
    public int ColumnEnd { get; set; } = 2;
    public int RowStart { get; set; } = 1;
    public int RowEnd { get; set; } = 2;
}
