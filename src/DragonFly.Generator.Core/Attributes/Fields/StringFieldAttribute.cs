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
        schema.AddField(
                                name: property,
                                fieldType: typeof(StringField),
                                options: new StringFieldOptions()
                                {
                                    IsSearchable = Searchable,
                                    IsRequired = Required,
                                    DefaultValue = DefaultValue,
                                    MinLength = MinLength,
                                    MaxLength = MaxLength
                                },
                                sortkey: schema.Fields.Count
                                );

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
