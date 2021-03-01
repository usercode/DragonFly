using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentField
    /// </summary>
    public abstract class SingleValueContentField<T> : ContentField, ISingleValueContentField       
    {
        public SingleValueContentField()
        {

        }

        private T _value;

        /// <summary>
        /// Value
        /// </summary>
        public T Value 
        {
            get => _value;
            set
            {
                OnValueChanging(ref value);

                _value = value;
            }
        }

        public bool HasValue => Value != null;

        object ISingleValueContentField.Value => Value;

        protected virtual void OnValueChanging(ref T newValue)
        {
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
