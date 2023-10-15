// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class IntegerFieldAttribute : BaseFieldAttribute
{
    public IntegerFieldAttribute()
    {
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        base.AddToSchema(schema, property);

        schema.AddInteger(property, x =>
                                        {
                                            x.IsRequired = Required;
                                            x.IsSearchable = Index;
                                        },
                                        SortKey);
    }
}
