﻿// Copyright (c) usercode
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