using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content;

/// <summary>
/// DateFieldOptions
/// </summary>
public class DateFieldOptions : ContentFieldOptions
{
    public DateFieldOptions()
    {
    }

    public override ContentField CreateContentField()
    {
        return new DateField();
    }
}
