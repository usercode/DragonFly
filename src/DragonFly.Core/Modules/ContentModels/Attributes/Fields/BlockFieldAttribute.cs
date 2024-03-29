﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class BlockFieldAttribute : BaseFieldAttribute
{
    public BlockFieldAttribute()
    {
    }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(BlockField),
                                options: new BlockFieldOptions(),
                                sortkey: schema.Fields.Count
                                );
    }
}
