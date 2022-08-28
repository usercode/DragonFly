using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content;

/// <summary>
/// TextField
/// </summary>
[FieldOptions(typeof(TextFieldOptions))]
public class TextField : TextBaseField
{
    public TextField()
    {

    }

    public TextField(string? text)
    {
        Value = text;
    }
}
