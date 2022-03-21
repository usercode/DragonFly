using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content;

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
