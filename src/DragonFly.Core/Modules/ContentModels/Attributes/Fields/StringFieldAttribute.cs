// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class StringFieldAttribute : BaseFieldAttribute
{
    public StringFieldAttribute()
    {
        MaxLength = StringFieldOptions.DefaultMaxLength;
    }

    public bool Searchable { get; set; }

    public string? DefaultValue { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        schema.AddString(property, x =>
                                        {
                                            x.IsSearchable = Searchable;
                                            x.IsRequired = Required;
                                            x.DefaultValue = DefaultValue;
                                            x.MinLength = MinLength;
                                            x.MaxLength = MaxLength;
                                        },
                                        SortKey);

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
