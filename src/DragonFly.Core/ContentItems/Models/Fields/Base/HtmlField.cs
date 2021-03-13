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

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            HtmlFieldOptions fieldOptions = (HtmlFieldOptions)options;

            if (fieldOptions.IsRequired && HasValue == false)
            {
                context.AddRequireValidation(fieldName);
            }
        }

        public HtmlField(string text)
        {
            Value = text;
        }

    }
}
