// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class HtmlFieldAttribute : BaseFieldAttribute
{
    public HtmlFieldAttribute(bool isRequired = false)
    {
        Required = isRequired;
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        schema.AddHtml(property, x =>
                                    {
                                        x.IsRequired = Required;
                                    },
                                    SortKey);
    }
}
