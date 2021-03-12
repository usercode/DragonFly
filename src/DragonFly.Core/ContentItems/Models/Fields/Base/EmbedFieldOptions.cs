using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public class EmbedFieldOptions : ContentFieldOptions
    {
        public override ContentField CreateContentField()
        {
            return new EmbedField();
        }
    }
}
