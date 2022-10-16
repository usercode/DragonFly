﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Proxy.Attributes;

public class AssetFieldAttribute : BaseFieldAttribute
{
    public AssetFieldAttribute()
    {
    }

    public bool ShowPreview { get; set; }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(AssetField), 
                                options: new AssetFieldOptions()
                                {
                                    IsRequired = Required,
                                    ShowPreview = ShowPreview
                                }, 
                                sortkey: schema.Fields.Count);

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
