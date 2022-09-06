using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public class SlugFieldOptions : StringFieldOptions
{
    public override IContentField CreateContentField()
    {
        return new SlugField(DefaultValue);
    }
}
