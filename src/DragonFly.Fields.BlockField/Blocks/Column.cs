using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// Column
/// </summary>
public class Column : IBlocksContent
{
    public Column()
    {
        Blocks = new List<Block>();
    }

    /// <summary>
    /// Width
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; }
}
