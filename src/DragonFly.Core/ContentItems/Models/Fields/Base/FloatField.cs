﻿using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// NumberPart
    /// </summary>
    [FieldOptions(typeof(FloatFieldOptions))]
    public class FloatField : SingleValueContentField<double?>
    {
        public FloatField()
        {

        }

        public FloatField(double? number)
        {
            Value = number;
        }

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            FloatFieldOptions fieldOptions = (FloatFieldOptions)options;

            if (fieldOptions != null)
            {
                if (fieldOptions.IsRequired && HasValue == false)
                {
                    context.AddRequireValidation(fieldName);
                }

                if (Value < fieldOptions.MinValue || Value > fieldOptions.MaxValue)
                {
                    context.AddRangeValidation(fieldName, fieldOptions.MinValue, fieldOptions.MaxValue);
                }                
            }
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
