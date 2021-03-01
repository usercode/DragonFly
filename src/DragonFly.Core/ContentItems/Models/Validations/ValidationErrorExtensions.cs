using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Validations
{
    public static class ValidationErrorExtensions
    {
        public static IList<ValidationError> AddRequire(this IList<ValidationError> errors, string field)
        {
            errors.Add(new ValidationError(field, $"The field '{field}' is required."));

            return errors;
        }

        public static IList<ValidationError> AddRange(this IList<ValidationError> errors, string field, double? from, double? to)
        {
            errors.Add(new ValidationError(field, $"The field '{field}' is out of range! The value must be between {from} and {to}."));

            return errors;
        }
        public static IList<ValidationError> AddMinimum(this IList<ValidationError> errors, string field, double? value)
        {
            errors.Add(new ValidationError(field, $"The field '{field}' must be at least {value} characters long."));

            return errors;
        }

        public static IList<ValidationError> AddMaximum(this IList<ValidationError> errors, string field, double? value)
        {
            errors.Add(new ValidationError(field, $"The field '{field}' can't be longer than {value} characters."));

            return errors;
        }

    }
}
