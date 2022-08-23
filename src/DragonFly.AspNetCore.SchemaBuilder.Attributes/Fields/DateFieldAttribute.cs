using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class DateFieldAttribute : BaseFieldAttribute
{
    public DateFieldAttribute()
    {
    }

    public override Type FieldType => typeof(DateTimeField);

    public override ContentFieldOptions CreateOptions()
    {
        return new DateTimeFieldOptions()
        {
            IsRequired = IsRequired
        };
    }
}
