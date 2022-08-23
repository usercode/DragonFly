using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class SlugFieldAttribute : BaseFieldAttribute
{
    public SlugFieldAttribute()
    {
    }

    public override Type FieldType => typeof(SlugField);

    public override ContentFieldOptions CreateOptions()
    {
        return new SlugFieldOptions()
        {
            IsRequired = IsRequired
        };
    }
}
