using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// QuoteBlock
/// </summary>
public class QuoteBlock : Block
{
    public override string CssIcon => "fa-solid fa-quote-left";

    public string? Text { get; set; }

    public string? Caption { get; set; }

    public string? Url { get; set; }
}
