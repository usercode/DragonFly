// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace DragonFly;

/// <summary>
/// ContentItemExtensions
/// </summary>
public static class ContentItemExtensions
{
    public static ContentField GetField(this IContentElement contentItem, string fieldName)
    {
        if (contentItem.TryGetField(fieldName, out ContentField? contentField) == false)
        {
            throw new Exception($"The field '{fieldName}' doesn't exist.");
        }

        return contentField;
    }

    public static T GetField<T>(this IContentElement contentItem, string name)
       where T : ContentField
    {
        return (T)GetField(contentItem, name);
    }

    public static bool TryGetField(this IContentElement contentElement, string name, [NotNullWhen(true)] out ContentField? contentField)
    {
        return contentElement.Fields.TryGetValue(name, out contentField);
    }

    public static void SetField(this IContentElement contentElement, string fieldName, ContentField contentField)
    {
        if (TrySetField(contentElement, fieldName, contentField) == false)
        {
            throw new Exception($"The field '{fieldName}' doesn't exist.");
        }
    }

    public static bool TrySetField(this IContentElement contentElement, string fieldName, ContentField contentField)
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
        ContentField field = GetField(contentItem, name);

        if (field is not SingleValueField<T> singleValueField)
        {
            throw new Exception($"The field '{name}' isn't a single value field.");
        }

        return singleValueField.Value;
    }

    public static T GetRequiredSingleValue<T>(this IContentElement contentItem, string name)
    {
        T? value = GetSingleValue<T>(contentItem, name);

        if (value == null)
        {
            throw new Exception($"The field '{name}' has no value.");
        }

        return value;
    }

    //public static T GetSingleValueOrDefault<T>(this IContentElement contentItem, string name) where T : struct
    //{
    //    T? value = GetSingleValue<T>(contentItem, name);

    //    if (value == null)
    //    {
    //        return default;
    //    }

    //    return value;
    //}

    public static void SetSingleValue<T>(this IContentElement contentItem, string name, T? value)
    {
        ((SingleValueField<T>)contentItem.GetField(name)).Value = value;
    }

    public static ContentItem CreateContent(this ContentSchema schema)
    {
        ContentItem item = new ContentItem(schema);

        ApplySchema(item, schema);

        return item;
    }

    public static ContentComponent CreateEmbeddedContent(this ContentSchema schema)
    {
        ContentComponent item = new ContentComponent(schema);

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
                ContentField? contentField;

                if (field.Value.Options != null)
                {
                    contentField = field.Value.Options.CreateContentField();
                }
                else
                {
                    contentField = FieldManager.Default.CreateField(field.Value.FieldType);
                }

                if (contentField == null)
                {
                    throw new Exception($"Could not create the content item: {field.Key}");
                }

                contentItem.Fields.Add(field.Key, contentField);
            }
        }
    }
}
