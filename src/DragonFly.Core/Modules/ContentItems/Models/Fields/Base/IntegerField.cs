// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

/// <summary>
/// IntegerField
/// </summary>
[Field]
[FieldOptions(typeof(IntegerFieldOptions))]
[FieldQuery(typeof(IntegerFieldQuery))]
public partial class IntegerField : SingleValueField<long?>
{
    public IntegerField()
    {

    }

    public override bool CanSorting => true;

    public IntegerField(long? number)
    {
        Value = number;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}
