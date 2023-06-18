// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator.Core.Attributes.Fields;

namespace DragonFly.Generator;

public class SlugFieldAttribute : BaseFieldAttribute
{
    public SlugFieldAttribute()
    {
    }

    public bool Index { get; set; }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(SlugField),
                                options: new SlugFieldOptions()
                                {
                                    IsRequired = Required,
                                    IsSearchable = Index
                                },
                                sortkey: schema.Fields.Count
                                );

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
