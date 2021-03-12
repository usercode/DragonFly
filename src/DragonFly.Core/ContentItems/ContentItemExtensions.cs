using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentItemExtensions
    /// </summary>
    public static class ContentItemExtensions
    {
        public static ContentField GetField(this IContentItem contentItem, string name)
        {
            if(contentItem.Fields.TryGetValue(name, out ContentField contentField) == false)
            {
                throw new Exception($"The field '{name}' was not found.");
            }

            return contentField;
        }

        public static T GetField<T>(this IContentItem contentItem, string name)
           where T : ContentField
        {
            return (T)GetField(contentItem, name);
        }

        public static T GetSingleValueField<T>(this IContentItem contentItem, string name)
        {
            return ((SingleValueContentField<T>)contentItem.Fields[name]).Value;
        }

        public static ContentItem CreateContentItem(this ContentSchema schema)
        {
            ContentItem item = new ContentItem();
            item.Schema = schema;

            ApplySchema(item, schema);

            return item;
        }

        public static ContentEmbedded CreateContentEmbedded(this ContentSchema schema)
        {
            ContentEmbedded item = new ContentEmbedded();
            item.Schema = schema;

            ApplySchema(item, schema);

            return item;
        }

        public static ArrayFieldItem CreateArrayField(this ArrayFieldOptions options)
        {
            ArrayFieldItem item = new ArrayFieldItem();

            ApplySchema(item, options);

            return item;
        }

        public static void ApplySchema(this ContentItem contentItem)
        {
            ApplySchema(contentItem, contentItem.Schema);

            contentItem.SchemaVersion = contentItem.Schema.Version;
        }

        public static void ApplySchema(this IContentItem contentItem, IContentSchema schema)
        {
            //remove content fields by schema
            foreach (var field in contentItem.Fields.ToList())
            {
                if (schema.Fields.ContainsKey(field.Key) == false)
                {
                    contentItem.Fields.Remove(field.Key);
                }
            }

            //apply schema to array fields
            foreach (var field in contentItem.Fields)
            {
                if (field.Value is ArrayField arrayField)
                {
                    ArrayFieldOptions options = (ArrayFieldOptions)schema.Fields[field.Key].Options;

                    foreach (ArrayFieldItem item in arrayField.Items)
                    {
                        ApplySchema(item, options);
                    }
                }
            }

            //add content field by schema
            foreach (var field in schema.Fields)
            {
                if (contentItem.Fields.ContainsKey(field.Key) == false)
                {
                    ContentField c;

                    if (field.Value.Options != null)
                    {
                        c = field.Value.Options.CreateContentField();
                    }
                    else
                    {
                        c = ContentFieldManager.Default.CreateField(field.Value.FieldType);
                    }

                    contentItem.Fields.Add(field.Key, c);
                }
            }
        }
    }
}
