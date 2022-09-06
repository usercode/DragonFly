using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public class ReferenceFieldOptions : ContentFieldOptions
{


    public override IContentField CreateContentField()
    {
        return new ReferenceField();
    }
}
