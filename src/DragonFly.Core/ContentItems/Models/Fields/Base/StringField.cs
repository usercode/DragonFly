using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// TextField
    /// </summary>
    [FieldOptions(typeof(StringFieldOptions))]
    public class StringField : TextBaseField
    {
        public StringField()
        {
        }

        public StringField(string text)
        {
            Value = text;
        }

        public override ContentFieldOptions CreateOptions()
        {
            return new StringFieldOptions();
        }

        public override bool CanSorting => true;
    }
}
