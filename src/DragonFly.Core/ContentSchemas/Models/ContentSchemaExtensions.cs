namespace DragonFly;

/// <summary>
/// ContentSchemaExtensions
/// </summary>
public static class ContentSchemaExtensions
{
    public static TContentSchema AddOrUpdateField<TContentSchema>(this TContentSchema schema, string name, Type fieldType, ContentFieldOptions? options = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        if (options == null)
        {
            options = ContentFieldManager.Default.CreateOptions(fieldType);
        }

        if (fieldType.IsSubclassOf(typeof(ContentField)) == false)
        {
            throw new Exception($"FieldType isn't a valid: {fieldType.Name}");
        }

        schema.Fields[name] = new SchemaField(ContentFieldManager.Default.GetContentFieldName(fieldType), options) { SortKey = sortkey, Options = options };       

        return schema;
    }

    public static ISchemaElement AddOrUpdateField<TField>(this ISchemaElement schema, string name, ContentFieldOptions? options = null, int sortkey = 0)
        where TField : IContentField
    {
        return AddOrUpdateField(schema, name, typeof(TField), options, sortkey);
    }

    public static TContentSchema RemoveField<TContentSchema>(this TContentSchema schema, string name)
        where TContentSchema : ISchemaElement
    {
        schema.Fields.Remove(name);

        return schema;
    }
}
