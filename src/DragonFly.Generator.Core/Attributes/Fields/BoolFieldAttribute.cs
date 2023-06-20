// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class BoolFieldAttribute : BaseFieldAttribute
{
    public bool Index { get; set; }

    public override void AddToSchema(ContentSchema schema, string property)
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
