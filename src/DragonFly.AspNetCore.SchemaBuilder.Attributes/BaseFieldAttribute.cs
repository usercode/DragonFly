using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class BaseFieldAttribute : Attribute
{
    public bool IsRequired { get; protected set; }

    public abstract Type FieldType { get; }

    public abstract ContentFieldOptions CreateOptions();
}
