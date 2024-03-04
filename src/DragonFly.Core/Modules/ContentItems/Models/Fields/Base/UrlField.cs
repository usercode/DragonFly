// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// UrlField
/// </summary>
[Field]
[FieldOptions(typeof(UrlFieldOptions))]
public partial class UrlField : TextBaseField
{
    public UrlField()
    {

    }

    public UrlField(string? text)
    {
        Value = text;
    }

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        base.Validate(fieldName, options, context);

        if (HasValue)
        {
            if (Uri.IsWellFormedUriString(Value, UriKind.Absolute) == false)
            {
                context.AddInvalidValidation(fieldName);
            }
        }
    }
}
