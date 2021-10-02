using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// DateFieldOptions
    /// </summary>
    public class DateTimeFieldOptions : ContentFieldOptions
    {
        public DateTimeFieldOptions()
        {
        }

        public override ContentField CreateContentField()
        {
            return new DateTimeField();
        }
    }
}
