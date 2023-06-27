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
