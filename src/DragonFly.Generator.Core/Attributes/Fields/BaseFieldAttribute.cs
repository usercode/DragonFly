// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator.Core.Attributes.Fields;

[AttributeUsage(AttributeTargets.Field)]
public abstract class BaseFieldAttribute : Attribute
{
    public bool Required { get; set; }

    public bool ListField { get; set; }

    public abstract void ApplySchema(string property, ContentSchema schema);
}
