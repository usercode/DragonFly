// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class DateFieldAttribute : BaseFieldAttribute
{
    public DateFieldAttribute()
    {
    }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddDateTime(property, x =>
                                    {
                                        x.IsRequired = Required;
                                        x.IsSearchable = Index;
                                    },
                                    SortKey);
    }
}
