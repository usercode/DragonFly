// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class FloatFieldAttribute : BaseFieldAttribute
{
    public FloatFieldAttribute()
    {
    }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddFloat(property, x =>
                                        {
                                            x.IsRequired = Required;
                                            x.HasIndex = Index;
                                        },
                                        SortKey);
    }
}
