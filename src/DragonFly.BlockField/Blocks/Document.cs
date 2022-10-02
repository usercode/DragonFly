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
/// Document
/// </summary>
public class Document : IChildBlocks
{
    public Document()
    {
        Blocks = new List<Block>();
    }

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; }

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
