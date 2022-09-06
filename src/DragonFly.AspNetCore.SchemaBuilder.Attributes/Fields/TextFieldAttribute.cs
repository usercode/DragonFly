using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class TextFieldAttribute : BaseFieldAttribute
{
    public TextFieldAttribute()
    {
    }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddOrUpdateField(
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
