// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
