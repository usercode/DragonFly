// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class UrlFieldAttribute : BaseFieldAttribute
{
    public UrlFieldAttribute()
    {
    }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddUrl(property, x =>
                                        {
                                            x.HasIndex = Index;
                                            x.IsRequired = Required;                                    
                                        },
                                        SortKey);
    }
}
