// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
