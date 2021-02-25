using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// HtmlField
    /// </summary>
    [FieldOptions(typeof(HtmlFieldOptions))]
    public class HtmlField : TextBaseField
    {
        public HtmlField()
        {

        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            HtmlFieldOptions fieldOptions = (HtmlFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && HasValue == false)
            {
                errors.AddRequire(fieldName);
            }

            return errors;
        }

        public HtmlField(string text)
        {
            Value = text;
        }

    }
}
