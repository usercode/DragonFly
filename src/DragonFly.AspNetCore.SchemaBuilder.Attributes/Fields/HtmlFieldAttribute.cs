using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class HtmlFieldAttribute : BaseFieldAttribute
{
    public HtmlFieldAttribute(bool isRequired = false)
    {
        Required = isRequired;
    }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddOrUpdateField(
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
