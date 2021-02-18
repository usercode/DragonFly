using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public abstract class ContentFieldOptions
    {
        public abstract ContentField CreateContentField();

        public virtual void ValidateContentField(ContentField contentField)
        {

        }
    }
}
