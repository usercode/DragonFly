using System.Collections.Generic;

namespace DragonFly.BlockField;

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
