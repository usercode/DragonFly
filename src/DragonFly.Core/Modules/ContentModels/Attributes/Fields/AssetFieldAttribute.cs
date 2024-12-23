// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class AssetFieldAttribute : BaseFieldAttribute
{
    public AssetFieldAttribute()
    {
    }

    public bool ShowPreview { get; set; }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddAsset(property, x =>
                                    {
                                        x.IsRequired = Required;
                                        x.ShowPreview = ShowPreview;
                                        x.HasIndex = Index;
                                    }, 
                                    SortKey);
    }
}
