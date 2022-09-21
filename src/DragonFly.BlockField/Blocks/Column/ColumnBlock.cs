﻿using System.Collections.Generic;

namespace DragonFly.BlockField;

/// <summary>
/// ColumnBlock
/// </summary>
public class ColumnBlock : Block
{
    public ColumnBlock()
    {
        Columns = new List<Column>();
    }

    public override string CssIcon => "fa-solid fa-table-columns";

    /// <summary>
    /// Columns
    /// </summary>
    public IList<Column> Columns { get; set; }
}