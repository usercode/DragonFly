using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// OpenGraphBlock
/// </summary>
public class OpenGraphBlock : Block
{
    public override string CssIcon => "fa-solid fa-globe";

    public string? Url { get; set; }
}
