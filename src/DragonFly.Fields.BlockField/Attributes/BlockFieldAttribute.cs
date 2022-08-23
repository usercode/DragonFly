using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class BlockFieldAttribute : BaseFieldAttribute
{
    public BlockFieldAttribute()
    {
    }

    public override Type FieldType => typeof(BlockField);

    public override ContentFieldOptions CreateOptions()
    {
        return new BlockFieldOptions() 
        {
        };
    }
}
