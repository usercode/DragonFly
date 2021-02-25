using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ArrayField
    /// </summary>
    [FieldOptions(typeof(ArrayFieldOptions))]
    public class ArrayField : ContentField
    {
        public ArrayField()
        {
            Items = new List<ArrayFieldItem>();
        }

        /// <summary>
        /// Items
        /// </summary>
        public IList<ArrayFieldItem> Items { get; set; }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            ArrayFieldOptions fieldOptions = (ArrayFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && Items.Any() == false)
            {
                errors.AddRequire(fieldName);
            }

            if (Items.Count < fieldOptions.MinItems || Items.Count > fieldOptions.MaxItems)
            {
                errors.AddRange(fieldName, fieldOptions.MinItems, fieldOptions.MaxItems);
            }

            return errors;
        }
    }
}
