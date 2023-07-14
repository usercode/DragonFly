// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentSchemaExtensions
/// </summary>
public static class ContentSchemaExtensions
{
    public static TContentSchema AddField<TContentSchema>(this TContentSchema schema, string name, Type fieldType, FieldOptions? options = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        if (options == null)
        {
            options = FieldManager.Default.CreateOptions(fieldType);
        }

        if (fieldType.IsSubclassOf(typeof(ContentField)) == false)
        {
            throw new Exception($"The field type '{fieldType.Name}' isn't valid.");
        }

        schema.Fields[name] = new SchemaField(FieldManager.Default.GetFieldName(fieldType), options) { SortKey = sortkey, Options = options };

        return schema;
    }

    public static TContentSchema RemoveField<TContentSchema>(this TContentSchema schema, string name)
        where TContentSchema : ISchemaElement
    {
        schema.Fields.Remove(name);

        return schema;
    }
}
