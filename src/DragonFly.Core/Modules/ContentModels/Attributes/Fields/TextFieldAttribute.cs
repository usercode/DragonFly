// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class TextFieldAttribute : BaseFieldAttribute
{
    public TextFieldAttribute()
    {
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        schema.AddTextArea(property, x =>
                                        {
                                            x.IsRequired = Required;
                                        },
                                        SortKey);
    }
}
