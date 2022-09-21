﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Proxy.Attributes;

namespace DragonFly.BlockField;

public class BlockFieldAttribute : BaseFieldAttribute
{
    public BlockFieldAttribute()
    {
    }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(BlockField),
                                options: new BlockFieldOptions()
                                {
                                },
                                sortkey: schema.Fields.Count
                                );
    }
}