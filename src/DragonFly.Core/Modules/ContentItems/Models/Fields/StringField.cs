// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

/// <summary>
/// TextField
/// </summary>
[Field]
[FieldOptions(typeof(StringFieldOptions))]
[FieldQuery(typeof(StringFieldQuery))]
public partial class StringField : TextBaseField
{
    public StringField()
    {
    }

    public StringField(string? text)
    {
        Value = text;
    }

    protected override void OnValueChanging(ref string? newValue)
    {
        base.OnValueChanging(ref newValue);

        if (newValue != null)
        {
            //replace line breaks
            newValue = newValue.Replace("\r\n", " ").Replace("\n", " ").Trim();
        }
    }

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        base.Validate(fieldName, options, context);

        if (HasValue)
        {
            if (options is StringFieldOptions stringOptions)
            {
                if (Value.Length < stringOptions.MinLength)
                {
                    context.AddMinimumValidation(fieldName, stringOptions.MinLength);
                }
                else if (Value.Length > stringOptions.MaxLength)
                {
                    context.AddMaximumValidation(fieldName, stringOptions.MaxLength);
                }
            }
        }
    }
}
