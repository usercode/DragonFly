using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// TextBlock
/// </summary>
public class TextBlock : Block
{
    public override string CssIcon => "bi bi-card-text";

    public string? Text { get; set; }
}
