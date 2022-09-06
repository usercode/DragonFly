using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class DateFieldAttribute : BaseFieldAttribute
{
    public DateFieldAttribute()
    {
    }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddOrUpdateField(
                                name: property,
                                fieldType: typeof(DateTimeField),
                                options: new DateTimeFieldOptions()
                                {
                                    IsRequired = Required
                                },
                                sortkey: schema.Fields.Count
                                );

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
