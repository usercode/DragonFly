using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class HtmlFieldAttribute : BaseFieldAttribute
{
    public HtmlFieldAttribute(bool isRequired = false)
    {
        IsRequired = isRequired;
    }

    public override Type FieldType => typeof(HtmlField);

    public override ContentFieldOptions CreateOptions()
    {
        return new HtmlFieldOptions()
        {
            IsRequired = IsRequired
        };
    }
}
