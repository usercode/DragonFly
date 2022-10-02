// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// ContentItemExtensions
/// </summary>
public static class ContentItemExtensions
{
    public static IContentField GetField(this IContentElement contentItem, string name)
    {
        if (contentItem.TryGetField(name, out IContentField? contentField) == false)
        {
            throw new Exception($"The field '{name}' was not found.");
        }

        return contentField;
    }

    public static T GetField<T>(this IContentElement contentItem, string name)
       where T : IContentField
    {
        return (T)GetField(contentItem, name);
    }

    public static bool TryGetField(this IContentElement contentElement, string name, [NotNullWhen(true)] out IContentField? contentField)
    {
        if (contentElement.Fields.TryGetValue(name, out contentField) == false)
        {
            return false;
        }

        return true;
    }

    public static bool TrySetField(this IContentElement contentElement, string fieldName, IContentField contentField)
    {
        if (contentElement.Fields.ContainsKey(fieldName) == false)
        {
            return false;
        }

        contentElement.Fields[fieldName] = contentField;

        return true;
    }

    public static T? GetSingleValue<T>(this IContentElement contentItem, string name)
    {
        return ((SingleValueField<T>)contentItem.Fields[name]).Value;
    }

    public static void SetSingleValue<T>(this IContentElement contentItem, string name, T? value)
    {
        ((SingleValueField<T>)contentItem.Fields[name]).Value = value;
    }

    public static ContentItem CreateContent(this ContentSchema schema)
    {
        ContentItem item = new ContentItem(schema);

        ApplySchema(item, schema);

        return item;
    }

    public static ContentEmbedded CreateEmbeddedContent(this ContentSchema schema)
    {
        ContentEmbedded item = new ContentEmbedded(schema);

        ApplySchema(item, schema);

        return item;
    }

    public static ArrayFieldItem CreateArrayField(this ArrayFieldOptions options)
    {
        ArrayFieldItem item = new ArrayFieldItem();

        ApplySchema(item, options);

        return item;
    }

    public static void ApplySchema(this ContentItem? contentItem)
    {
        if (contentItem == null)
        {
            throw new ArgumentNullException(nameof(contentItem));
        }

        ApplySchema(contentItem, contentItem.Schema);

        contentItem.SchemaVersion = contentItem.Schema.Version;
    }

    public static void ApplySchema(this IContentElement contentItem, ISchemaElement schema)
    {
        if (contentItem == null)
        {
            throw new ArgumentNullException(nameof(contentItem));
        }

        if (schema == null)
        {
            throw new ArgumentNullException(nameof(schema));
        }

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
                ArrayFieldOptions? options = (ArrayFieldOptions?)schema.Fields[field.Key].Options;

                if (options == null)
                {
                    throw new Exception("ArrayFieldOptions are not available.");
                }

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
                IContentField? contentField;

                if (field.Value.Options != null)
                {
                    contentField = field.Value.Options.CreateContentField();
                }
                else
                {
                    contentField = ContentFieldManager.Default.CreateField(field.Value.FieldType);
                }

                if (contentField == null)
                {
                    throw new Exception($"Could not create the content item. {field.Key}");
                }

                contentItem.Fields.Add(field.Key, contentField);
            }
        }
    }
}
