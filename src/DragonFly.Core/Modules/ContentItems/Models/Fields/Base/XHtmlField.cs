// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Xml.Linq;
using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// TextField
/// </summary>
[FieldOptions(typeof(XHtmlFieldOptions))]
public class XHtmlField : TextBaseField
{
    public XHtmlField()
    {

    }

    public XHtmlField(string? text)
    {
        Value = text;
    }

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        if (HasValue)
        {
            try
            {
                XElement.Parse($"<div>{Value}</div>");
            }
            catch (Exception ex)
            {
                context.AddInvalidValidation(fieldName);
            }
        }
    }
}
