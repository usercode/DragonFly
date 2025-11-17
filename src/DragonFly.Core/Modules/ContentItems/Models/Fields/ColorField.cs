// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using System.Text.RegularExpressions;

namespace DragonFly;

/// <summary>
/// ColorField
/// </summary>
[Field]
[FieldOptions(typeof(ColorFieldOptions))]
public partial class ColorField : TextBaseField
{
    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        base.Validate(fieldName, options, context);

        if (HasValue)
        {
            if (RegexHex().IsMatch(Value) == false)
            {
                context.AddInvalidValidation(nameof(Value));
            }
        }
    }

    [GeneratedRegex("^#[a-f0-9]{6}$")]
    private static partial Regex RegexHex();
}
