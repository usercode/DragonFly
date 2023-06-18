// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator.Core.Attributes.Fields;

namespace DragonFly.Generator;

public class DateFieldAttribute : BaseFieldAttribute
{
    public DateFieldAttribute()
    {
    }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(DateTimeField),
                                options: new DateTimeFieldOptions()
                                {
                                    IsRequired = Required
                                },
                                sortkey: schema.Fields.Count
                                );

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
