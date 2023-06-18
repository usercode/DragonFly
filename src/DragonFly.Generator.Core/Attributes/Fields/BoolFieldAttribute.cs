// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator.Core.Attributes.Fields;

namespace DragonFly.Generator;

public class BoolFieldAttribute : BaseFieldAttribute
{
    public bool Index { get; set; }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(BoolField),
                                options: new BoolFieldOptions()
                                {
                                    DefaultValue = false,
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
