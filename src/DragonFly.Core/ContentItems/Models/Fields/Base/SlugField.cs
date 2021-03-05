using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// SlugField
    /// </summary>
    [FieldOptions(typeof(SlugFieldOptions))]
    public class SlugField : TextBaseField
    {
        public SlugField()
        {

        }

        public override bool CanSorting => true;

        public SlugField(string text)
        {
            Value = text;
        }

        protected override void OnValueChanging(ref string newValue)
        {
            //if (newValue != null)
            //{
            //    newValue = Slugify.ToSlug(newValue);
            //}
        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            SlugFieldOptions fieldOptions = (SlugFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && HasValue == false)
            {
                errors.AddRequire(fieldName);
            }

            return errors;
        }
    }
}
