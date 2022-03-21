using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content;

/// <summary>
/// TextField
/// </summary>
[FieldOptions(typeof(TextAreaFieldOptions))]
public class TextAreaField : TextBaseField
{
    public TextAreaField()
    {

    }

    public TextAreaField(string text)
    {
        Value = text;
    }
}
