﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class FloatFieldAttribute : BaseFieldAttribute
{
    public FloatFieldAttribute()
    {
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        base.AddToSchema(schema, property);

        schema.AddFloat(property, x =>
                                        {
                                            x.IsRequired = Required;
                                            x.IsSearchable = Index;
                                        },
                                        SortKey);
    }
}
