using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class BaseFieldAttribute : Attribute
{
    public bool Required { get; set; }

    public bool ListField { get; set; }

    public abstract void ApplySchema(string property, ContentSchema schema);
}
