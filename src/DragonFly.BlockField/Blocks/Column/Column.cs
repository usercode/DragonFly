﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

/// <summary>
/// Column
/// </summary>
public class Column : IBlocksContent
{
    public Column()
    {
        Width = ColumnWidth.Max;
        Blocks = new List<Block>();
    }

    /// <summary>
    /// Width
    /// </summary>
    public ColumnWidth Width { get; set; }

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; }
}