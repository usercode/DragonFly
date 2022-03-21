using DragonFly.Content;
using System.Collections.Generic;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// BlockField
/// </summary>
[FieldOptions(typeof(BlockFieldOptions))]
public class BlockField : SingleValueContentField<string>
{
    public BlockField()
    {
    }
}
