using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// SingleValueContentField
    /// </summary>
    public abstract class SingleValueContentField<T> : ContentField, ISingleValueContentField       
    {
        public SingleValueContentField()
        {

        }

        private T? _value;

        /// <summary>
        /// Value
        /// </summary>
        public virtual T? Value 
        {
            get => _value;
            set
            {
                OnValueChanging(ref value);

                _value = value;
            }
        }

        [MemberNotNullWhen(returnValue: true, member: nameof(Value))]
        public bool HasValue => Value != null;

        object? ISingleValueContentField.Value => Value;

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            if (options.IsRequired && HasValue == false)
            {
                context.AddRequireValidation(fieldName);
            }
        }

        protected virtual void OnValueChanging(ref T? newValue)
        {
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
