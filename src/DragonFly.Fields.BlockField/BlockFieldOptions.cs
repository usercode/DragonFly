using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

public class BlockFieldOptions : ContentFieldOptions
{
    public override IContentField CreateContentField()
    {
        return new BlockField();
    }
}
