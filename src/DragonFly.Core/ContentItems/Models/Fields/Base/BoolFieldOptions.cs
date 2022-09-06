using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public class BoolFieldOptions : SingleValueFieldOptions<bool>
{
    public override IContentField CreateContentField()
    {
        return new BoolField(DefaultValue);
    }
}
