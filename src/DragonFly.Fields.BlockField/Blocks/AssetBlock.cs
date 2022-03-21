using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Blocks;

/// <summary>
/// AssetBlock
/// </summary>
public class AssetBlock : Block
{
    public Guid? AssetId { get; set; }
}
