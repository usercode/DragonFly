// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
            if (options is UrlFieldOptions urlOptions)
            {
                UriKind? uriKind = urlOptions.UrlType switch
                {
                    UrlType.Absolute => UriKind.Absolute,
                    UrlType.Relative => UriKind.Relative,
                    _ => null
                };

                if (uriKind != null)
                {
                    if (Uri.IsWellFormedUriString(Value, uriKind.Value) == false)
                    {
                        context.AddInvalidValidation(fieldName);
                    }
                }
            }
        }
    }
}
