using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// ValidationExtensions
/// </summary>
public static class ValidationExtensions
{
    public static ValidationContext Validate(this ContentItem contentItem)
    {
        ValidationContext validationContext = new ValidationContext();

        foreach (var field in contentItem.Fields)
        {
            SchemaField f = contentItem.Schema.Fields[field.Key];

            if (f.Options != null)
            {
                field.Value.Validate(field.Key, f.Options, validationContext);
            }
        }

        return validationContext;
    }
}
