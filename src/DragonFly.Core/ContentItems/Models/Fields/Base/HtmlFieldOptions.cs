using DragonFly.Contents.Content.Fields;
using DragonFly.Contents.Content.Parts.Base;
using DragonFly.Contents.Content.Schemas;
using DragonFly.Data.Content.ContentParts;
using DragonFly.Data.Content.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content.ContentParts
{
    /// <summary>
    /// HtmlFieldOptions
    /// </summary>
    public class HtmlFieldOptions : ContentFieldOptions
    {
        public HtmlFieldOptions()
        {
        }

        public override ContentField CreateContentField()
        {
            return new HtmlField();
        }
    }
}
