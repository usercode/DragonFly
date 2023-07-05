// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class ReferenceFieldAttribute : BaseFieldAttribute
{
    public ReferenceFieldAttribute()
    {
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        schema.AddReference(property, x =>
                                        {
                                            x.IsRequired = Required;
                                        },
                                        SortKey);

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
