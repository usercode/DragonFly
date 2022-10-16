// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// BoolField
/// </summary>
[FieldOptions(typeof(BoolFieldOptions))]
public class BoolField : SingleValueField<bool?>
{
    public BoolField()
    {

    }

    public override bool CanSorting => true;

    public BoolField(bool? value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}
