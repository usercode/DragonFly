// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

[AttributeUsage(AttributeTargets.Field)]
public abstract class BaseFieldAttribute : Attribute
{
    public bool Required { get; set; }

    public bool ListField { get; set; }

    public bool QueryField { get; set; }

    public bool ReferenceField { get; set; }

    public int SortKey { get; set; }

    public bool Index { get; set; }

    public virtual void ApplyToSchema(ContentSchema schema, string property)
    {
        if (ListField)
        {
            schema.ListFields.Add(property);
        }

        if (QueryField)
        {
            schema.QueryFields.Add(property);
        }

        if (ReferenceField)
        {
            schema.ReferenceFields.Add(property);
        }
    }
}
