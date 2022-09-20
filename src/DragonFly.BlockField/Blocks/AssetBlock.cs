using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

/// <summary>
/// AssetBlock
/// </summary>
public class AssetBlock : Block
{
    public override string CssIcon => "fa-regular fa-image";

    public Guid? AssetId { get; set; }
}
