﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class StringFieldAttribute : BaseFieldAttribute
{
    public StringFieldAttribute()
    {
        MaxLength = StringFieldOptions.DefaultMaxLength;
    }

    public string? DefaultValue { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddString(property, x =>
                                        {
                                            x.HasIndex = Index;
                                            x.IsRequired = Required;
                                            x.DefaultValue = DefaultValue;
                                            x.MinLength = MinLength;
                                            x.MaxLength = MaxLength;
                                        },
                                        SortKey);
    }
}
