// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator.Core.Attributes.Fields;

namespace DragonFly.Generator;

public class HtmlFieldAttribute : BaseFieldAttribute
{
    public HtmlFieldAttribute(bool isRequired = false)
    {
        Required = isRequired;
    }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(HtmlField),
                                options: new HtmlFieldOptions()
                                {
                                    IsRequired = Required
                                },
                                sortkey: schema.Fields.Count
                                );
    }
}
