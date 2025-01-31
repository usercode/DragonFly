﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class ReferenceFieldAttribute : BaseFieldAttribute
{
    public ReferenceFieldAttribute()
    {
    }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddReference(property, x =>
                                        {
                                            x.IsRequired = Required;
                                            x.HasIndex = Index;
                                        },
                                        SortKey);
    }
}
