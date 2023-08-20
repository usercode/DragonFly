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
        base.AddToSchema(schema, property);

        schema.AddReference(property, x =>
                                        {
                                            x.IsRequired = Required;
                                            x.IsSearchable = Index;
                                        },
                                        SortKey);
    }
}
