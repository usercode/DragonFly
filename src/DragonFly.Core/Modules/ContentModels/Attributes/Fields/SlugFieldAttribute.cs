// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class SlugFieldAttribute : BaseFieldAttribute
{
    public SlugFieldAttribute()
    {
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        base.AddToSchema(schema, property);

        schema.AddSlug(property, x =>
                                    {
                                        x.IsRequired = Required;
                                        x.IsSearchable = Index;
                                    },
                                    SortKey);       
    }
}
