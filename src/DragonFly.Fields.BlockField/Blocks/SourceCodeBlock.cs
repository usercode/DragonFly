using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// SourceCodeBlock
/// </summary>
public class SourceCodeBlock : Block
{
    /// <summary>
    /// SourceCodeType
    /// </summary>
    public SourceCodeType SourceCodeType { get; set; }

    /// <summary>
    /// SourceCode
    /// </summary>
    public string? SourceCode { get; set; }
}
