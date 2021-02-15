using DragonFly.Core.ContentItems.Models.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content.ContentParts
{
    /// <summary>
    /// HtmlField
    /// </summary>
    [FieldOptions(typeof(HtmlFieldOptions))]
    public class HtmlField : TextBaseField
    {
        public HtmlField()
        {

        }

        public HtmlField(string text)
        {
            Value = text;
        }

    }
}
