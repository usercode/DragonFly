using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class StringFieldAttribute : BaseFieldAttribute
{
    public StringFieldAttribute()
    {
        MaxLength = StringFieldOptions.DefaultMaxLength;
    }

    public bool Index { get; set; }

    public string? DefaultValue { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public override void ApplySchema(string property, ContentSchema schema)
    {
        schema.AddOrUpdateField(
                                name: property,
                                fieldType: typeof(StringField),
                                options: new StringFieldOptions()
                                {
                                    IsSearchable = Index,
                                    IsRequired = Required,
                                    DefaultValue = DefaultValue,
                                    MinLength = MinLength,
                                    MaxLength = MaxLength
                                },
                                sortkey: schema.Fields.Count
                                );

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
