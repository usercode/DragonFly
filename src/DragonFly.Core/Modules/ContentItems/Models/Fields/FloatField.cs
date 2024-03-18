// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// FloatField
/// </summary>
[Field]
[FieldOptions(typeof(FloatFieldOptions))]
[FieldQuery(typeof(FloatFieldQuery))]
public partial class FloatField : SingleValueField<double?>
{
    public FloatField()
    {

    }

    public FloatField(double? number)
    {
        Value = number;
    }

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        if (options is FloatFieldOptions fieldOptions)
        {
            if (fieldOptions.IsRequired && HasValue == false)
            {
                context.AddRequireValidation(fieldName);
            }

            if (Value < fieldOptions.MinValue || Value > fieldOptions.MaxValue)
            {
                context.AddRangeValidation(fieldName, fieldOptions.MinValue, fieldOptions.MaxValue);
            }
        }
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}
