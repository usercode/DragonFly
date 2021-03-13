using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// BoolField
    /// </summary>
    [FieldOptions(typeof(BoolFieldOptions))]
    public class BoolField : SingleValueContentField<bool?>
    {
        public BoolField()
        {

        }

        public override bool CanSorting => true;

        public BoolField(bool? value)
        {
            Value = value;
        }

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            BoolFieldOptions fieldOptions = (BoolFieldOptions)options;

            if (fieldOptions.IsRequired && HasValue == false)
            {
                context.AddRequireValidation(fieldName);
            }
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
