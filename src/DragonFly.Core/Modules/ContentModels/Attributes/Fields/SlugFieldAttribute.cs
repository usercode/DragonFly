// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class SlugFieldAttribute : BaseFieldAttribute
{
    public SlugFieldAttribute()
    {
    }

    public string? TargetField { get; set; }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddSlug(property, x =>
                                    {
                                        x.IsRequired = Required;
                                        x.HasIndex = Index;
                                        x.TargetField = TargetField;
                                    },
                                    SortKey);
    }
}
