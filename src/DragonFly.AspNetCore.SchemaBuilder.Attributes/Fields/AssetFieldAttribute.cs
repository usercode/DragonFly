using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class AssetFieldAttribute : BaseFieldAttribute
{
    public AssetFieldAttribute(bool isRequired = false)
    {
        IsRequired = isRequired;
    }

    public override Type FieldType => typeof(AssetField);

    public override ContentFieldOptions CreateOptions()
    {
        return new AssetFieldOptions() 
        {
        };
    }
}
