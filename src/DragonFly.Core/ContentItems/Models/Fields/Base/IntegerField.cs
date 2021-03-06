﻿using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// IntegerField
    /// </summary>
    [FieldOptions(typeof(IntegerFieldOptions))]
    public class IntegerField : SingleValueContentField<long?>
    {
        public IntegerField()
        {

        }

        public override bool CanSorting => true;

        public IntegerField(long? number)
        {
            Value = number;
        }

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            IntegerFieldOptions fieldOptions = (IntegerFieldOptions)options;

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
