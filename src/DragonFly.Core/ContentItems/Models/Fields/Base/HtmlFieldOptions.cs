using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// HtmlFieldOptions
/// </summary>
public class HtmlFieldOptions : ContentFieldOptions
{
    public HtmlFieldOptions()
    {
    }

    public override IContentField CreateContentField()
    {
        return new HtmlField();
    }
}
