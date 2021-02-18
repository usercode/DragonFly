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
    public class BoolField : SingleValueContentFieldNullable<bool>
    {
        public BoolField()
        {

        }

        public override bool CanSorting => true;

        public BoolField(bool? value)
        {
            Value = value;
        }

        public override ContentFieldOptions CreateOptions()
        {
            return new BoolFieldOptions();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
