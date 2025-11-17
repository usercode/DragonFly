// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

/// <summary>
/// BoolField
/// </summary>
[Field]
[FieldOptions(typeof(BoolFieldOptions))]
[FieldQuery(typeof(BoolFieldQuery))]
public partial class BoolField : SingleValueField<bool?>
{
    public BoolField()
    {

    }

    public BoolField(bool? value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}
