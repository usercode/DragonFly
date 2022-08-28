using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

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

    public override Type FieldType => typeof(StringField);

    public override ContentFieldOptions CreateOptions()
    {
        return new StringFieldOptions() 
        {  
            IsSearchable = Index,
            IsRequired = IsRequired,
            DefaultValue = DefaultValue, 
            MinLength = MinLength, 
            MaxLength = MaxLength
        };
    }
}
