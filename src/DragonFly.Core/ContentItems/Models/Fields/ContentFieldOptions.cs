using DragonFly.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Data.Content.ContentParts
{
    public abstract class ContentFieldOptions
    {
        public abstract ContentField CreateContentField();

        public virtual void ValidateContentField(ContentField contentField)
        {

        }
    }
}
