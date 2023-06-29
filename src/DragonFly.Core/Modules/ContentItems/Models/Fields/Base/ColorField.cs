// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.RegularExpressions;
using DragonFly.Validations;

namespace DragonFly;

[FieldOptions(typeof(ColorFieldOptions))]
public partial class ColorField : TextBaseField
{
    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
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
