using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class TextFieldAttribute : BaseFieldAttribute
{
    public TextFieldAttribute()
    {
    }

    public override Type FieldType => typeof(TextField);

    public override ContentFieldOptions CreateOptions()
    {
        return new TextFieldOptions()
        {
            IsRequired = IsRequired
        };
    }
}
