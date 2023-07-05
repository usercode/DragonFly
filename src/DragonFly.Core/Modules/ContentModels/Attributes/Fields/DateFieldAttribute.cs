// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class DateFieldAttribute : BaseFieldAttribute
{
    public DateFieldAttribute()
    {
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        schema.AddDate(property, x =>
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
