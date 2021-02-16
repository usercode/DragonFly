using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content;
using DragonFly.Contents.Content.Fields;
using DragonFly.Contents.Content.Schemas;
using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Data.Content.ContentParts;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Models;

namespace DragonFly.Models
{
    /// <summary>
    /// ContentItemExtensions
    /// </summary>
    public static class ContentItemExtensions
    {
        public static ContentField GetField(this IContentItem contentItem, string name)
        {
            return contentItem.Fields[name];
        }

        public static T GetField<T>(this IContentItem contentItem, string name)
           where T : ContentField
        {
            return (T)GetField(contentItem, name);
        }

        public static T GetSingleField<T>(this IContentItem contentItem, string name)
        {
            return ((SingleValueContentField<T>)contentItem.Fields[name]).Value;
        }

        public static T GetSingleNullableField<T>(this IContentItem contentItem, string name)
            where T : struct
        {
            return ((SingleValueContentFieldNullable<T>)contentItem.Fields[name]).Value.Value;
        }

        public static IContentSchema AddField<TField>(this IContentSchema schema, string name, int sortkey = 0, ContentFieldOptions options = null)
            where TField : ContentField
        {
            schema.Fields.Add(name, new ContentFieldDefinition() { SortKey = sortkey, FieldType = ContentFieldManager.Default.GetContentFieldName<TField>(), Options = options });

            return schema;
        }

        public static ContentItem CreateItem(this ContentSchema schema)
        {
            ContentItem item = new ContentItem();
            item.Schema = schema;

            ApplySchema(item, schema);

            return item;
        }

        public static ArrayFieldItem CreateItem(this ArrayFieldOptions options)
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
                    ContentField c = ContentFieldManager.Default.CreateField(field.Value.FieldType);
                    if (field.Value.Options != null)
                    {
                        c = field.Value.Options.CreateContentField();
                    }

                    contentItem.Fields.Add(field.Key, c);
                }
            }
        }
    }
}
