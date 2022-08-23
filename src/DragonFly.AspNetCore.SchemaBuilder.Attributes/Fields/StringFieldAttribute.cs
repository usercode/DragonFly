using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

public class StringFieldAttribute : BaseFieldAttribute
{
    public StringFieldAttribute(bool isRequired = false, string? defaultValue = null, int minLength = 0, int maxLength = StringFieldOptions.DefaultMaxLength)
    {
        IsRequired = isRequired;
        DefaultValue = defaultValue;
        MinLength = minLength;
        MaxLength = maxLength;
    }

    public string? DefaultValue { get; }

    public int MinLength { get; }

    public int MaxLength { get; }

    public override Type FieldType => typeof(StringField);

    public override ContentFieldOptions CreateOptions()
    {
        return new StringFieldOptions() 
        {  
            IsRequired = IsRequired,
            DefaultValue = DefaultValue, 
            MinLength = MinLength, 
            MaxLength = MaxLength
        };
    }
}
