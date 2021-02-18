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
    public class FloatField : SingleValueContentFieldNullable<double>
    {
        public FloatField()
        {

        }

        public FloatField(double? number)
        {
            Value = number;
        }

        public override ContentFieldOptions CreateOptions()
        {
            return new FloatFieldOptions();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
