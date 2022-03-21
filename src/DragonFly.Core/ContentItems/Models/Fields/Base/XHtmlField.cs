using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content;

/// <summary>
/// TextField
/// </summary>
[FieldOptions(typeof(XHtmlFieldOptions))]
public class XHtmlField : TextBaseField
{
    public XHtmlField()
    {

    }

    public XHtmlField(string text)
    {
        Value = text;
    }
}
