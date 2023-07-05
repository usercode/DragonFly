// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class SlugFieldAttribute : BaseFieldAttribute
{
    public SlugFieldAttribute()
    {
    }

    public bool Index { get; set; }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        schema.AddSlug(property, x =>
                                    {
                                        x.IsRequired = Required;
                                        x.IsSearchable = Index;
                                    },
                                    SortKey);

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
