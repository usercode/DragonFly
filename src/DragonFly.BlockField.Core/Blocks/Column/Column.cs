﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// Column
/// </summary>
public class Column
{
    public Column()
    {
    }

    public Column(params Block[] blocks)
        : this()
    {
        Blocks = blocks;
    }

    /// <summary>
    /// Width
    /// </summary>
    public ColumnWidth Width { get; set; } = ColumnWidth.Max;

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; } = new List<Block>();
}
