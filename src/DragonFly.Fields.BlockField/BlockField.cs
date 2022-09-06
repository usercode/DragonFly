using System.Collections.Generic;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// BlockField
/// </summary>
[FieldOptions(typeof(BlockFieldOptions))]
public class BlockField : SingleValueField<string>
{
    public BlockField()
    {
    }
}
