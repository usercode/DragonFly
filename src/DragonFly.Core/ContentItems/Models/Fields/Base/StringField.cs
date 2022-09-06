using DragonFly.Core.ContentItems.Models.Validations;
using DragonFly.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// TextField
/// </summary>
[FieldOptions(typeof(StringFieldOptions))]
[FieldQuery(typeof(StringFieldQuery))]
public class StringField : TextBaseField
{
    public StringField()
    {
    }

    public StringField(string? text)
    {
        Value = text;
    }

    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        if (options is StringFieldOptions fieldOptions)
        {
            if (fieldOptions.IsRequired && HasValue == false)
            {
                context.AddRequireValidation(fieldName);
            }

            if (HasValue)
            {
                if (Value.Length < fieldOptions.MinLength)
                {
                    context.AddMinimumValidation(fieldName, fieldOptions.MinLength);
                }
                else if (Value.Length > fieldOptions.MaxLength)
                {
                    context.AddMaximumValidation(fieldName, fieldOptions.MaxLength);
                }
            }
        }
    }

    public override bool CanSorting => true;
}
