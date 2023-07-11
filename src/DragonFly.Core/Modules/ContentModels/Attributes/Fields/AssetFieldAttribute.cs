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

    public override void AddToSchema(ContentSchema schema, string property)
    {
        base.AddToSchema(schema, property);

        schema.AddAsset(property, x =>
                                    {
                                        x.IsRequired = Required;
                                        x.ShowPreview = ShowPreview;
                                    }, 
                                    SortKey);
    }
}
