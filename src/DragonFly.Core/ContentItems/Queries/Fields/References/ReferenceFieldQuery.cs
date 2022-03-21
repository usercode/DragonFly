using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content;

/// <summary>
/// ReferenceFieldQuery
/// </summary>
public class ReferenceFieldQuery : FieldQuery
{
    /// <summary>
    /// ContentItemId
    /// </summary>
    public Guid? ContentItemId { get; set; }

    public override bool IsEmpty() => ContentItemId == null;
}
