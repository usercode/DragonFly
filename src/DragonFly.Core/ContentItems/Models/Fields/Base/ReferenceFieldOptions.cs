using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content;

public class ReferenceFieldOptions : ContentFieldOptions
{


    public override IContentField CreateContentField()
    {
        return new ReferenceField();
    }
}
