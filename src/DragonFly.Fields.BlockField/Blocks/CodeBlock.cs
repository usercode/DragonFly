using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// CodeBlock
/// </summary>
public class CodeBlock : Block
{
    public override string CssIcon => "bi bi-code-square";

    /// <summary>
    /// CodeType
    /// </summary>
    public CodeType CodeType { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    public string? Content { get; set; }
}
