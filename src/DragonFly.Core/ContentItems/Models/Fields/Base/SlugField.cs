using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Core.ContentItems.Models.Fields.Base;
using DragonFly.Data.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content.ContentParts
{
    /// <summary>
    /// TextField
    /// </summary>
    [FieldOptions(typeof(SlugFieldOptions))]
    public class SlugField : TextBaseField
    {
        public SlugField()
        {

        }

        public override bool CanSorting => true;

        public SlugField(string text)
        {
            Value = text;
        }

        public override ContentFieldOptions CreateOptions()
        {
            return new SlugFieldOptions();
        }
    }
}
