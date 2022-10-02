// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

/// <summary>
/// Column
/// </summary>
public class Column
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
