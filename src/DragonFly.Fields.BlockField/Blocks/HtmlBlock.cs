using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// HtmlBlock
/// </summary>
public class HtmlBlock : Block
{
    public override string CssIcon => "fa-regular fa-file-code";

    public string? HtmlText { get; set; }
}
