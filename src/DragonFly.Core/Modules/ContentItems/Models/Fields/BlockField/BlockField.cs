// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// BlockField
/// </summary>
[Field]
[FieldOptions(typeof(BlockFieldOptions))]
public partial class BlockField : SingleValueField<string>
{
    public BlockField()
    {
    }
}
