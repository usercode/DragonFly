// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.RegularExpressions;
using DragonFly.Validations;

namespace DragonFly;

[FieldOptions(typeof(ColorFieldOptions))]
public class ColorField : TextBaseField
{
    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        if (HasValue)
        {
            if (Regex.IsMatch(Value, "^#[a-f0-9]{6}$", RegexOptions.Compiled) == false)
            {
                context.AddInvalidValidation(nameof(Value));
            }
        }
    }
}
