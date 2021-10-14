using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentSchemaExtensions
    /// </summary>
    public static class ContentSchemaExtensions
    {
        public static ISchemaElement AddField(this ISchemaElement schema, string name, Type fieldType, ContentFieldOptions? options = null, int sortkey = 0)
        {
            if (options == null)
            {
                options = ContentFieldManager.Default.CreateOptions(fieldType);
            }

            if (schema.Fields.TryAdd(name, new SchemaField(ContentFieldManager.Default.GetContentFieldName(fieldType), options) { SortKey = sortkey, Options = options }) == false)
            {
                throw new Exception($"The content schema already contains a field with the name '{name}'");
            }

            return schema;
        }

        public static ISchemaElement AddField<TField>(this ISchemaElement schema, string name, ContentFieldOptions? options = null, int sortkey = 0)
            where TField : ContentField
        {
            return AddField(schema, name, typeof(TField), options, sortkey);
        }

        public static TContentSchema RemoveField<TContentSchema>(this TContentSchema schema, string name)
            where TContentSchema : ISchemaElement
        {
            schema.Fields.Remove(name);

            return schema;
        }

        
    }
}
