using DragonFly.Content;
using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public static class ValidationExtensions
    {
        public static IEnumerable<ValidationError> Validate(this ContentItem contentItem)
        {
            List<ValidationError> result = new List<ValidationError>();

            foreach (var field in contentItem.Fields)
            {
                ContentSchemaField f = contentItem.Schema.Fields[field.Key];

                ValidationContext validationContext = new ValidationContext();

                field.Value.Validate(field.Key, f.Options, validationContext);

                result.AddRange(validationContext.Errors);
            }

            return result;
        }
    }
}
