using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Core.ContentItems.Models.Fields.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content.ContentParts
{
    /// <summary>
    /// TextField
    /// </summary>
    [FieldOptions(typeof(XmlFieldOptions))]
    public class XmlField : TextBaseField
    {
        public XmlField()
        {

        }

        public XmlField(string text)
        {
            Value = text;
        }
    }
}
