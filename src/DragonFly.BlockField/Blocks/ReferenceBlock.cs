using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

/// <summary>
/// ReferenceBlock
/// </summary>
public class ReferenceBlock : Block
{
    public override string CssIcon => "fa-solid fa-list";

    /// <summary>
    /// ContentId
    /// </summary>
    public Guid? ContentId { get; set; }

    /// <summary>
    /// ContentSchema
    /// </summary>
    public string? ContentSchema { get; set; }
}
