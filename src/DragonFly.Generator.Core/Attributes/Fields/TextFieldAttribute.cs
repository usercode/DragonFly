// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator.Core.Attributes.Fields;

namespace DragonFly.Generator;

public class TextFieldAttribute : BaseFieldAttribute
{
    public TextFieldAttribute()
    {
    }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddField(
                                name: property,
                                fieldType: typeof(TextField),
                                options: new TextFieldOptions()
                                {
                                    IsRequired = Required
                                },
                                sortkey: schema.Fields.Count
                                );
    }
}
