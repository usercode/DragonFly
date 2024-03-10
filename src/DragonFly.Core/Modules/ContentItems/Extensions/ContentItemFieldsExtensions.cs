// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentItemFieldsExtensions
/// </summary>
public static class ContentItemFieldsExtensions
{
    public static ContentItem? GetReference(this IContentElement contentItem, string name)
    {
        ReferenceField field = contentItem.GetField<ReferenceField>(name);

        return field.ContentItem;
    }

    public static TContentItem SetReference<TContentItem>(this TContentItem contentItem, string name, ContentItem? reference)
        where TContentItem : IContentElement
    {
        ReferenceField field = contentItem.GetField<ReferenceField>(name);

        field.ContentItem = reference;

        return contentItem;
    }

    public static Asset? GetAsset(this IContentElement contentItem, string name)
    {
        AssetField field = contentItem.GetField<AssetField>(name);

        return field.Asset;
    }

    public static TContentItem SetAsset<TContentItem>(this TContentItem contentItem, string name, Asset? asset)
        where TContentItem : IContentElement
    {
        AssetField field = contentItem.GetField<AssetField>(name);

        field.Asset = asset;

        return contentItem;
    }

    public static ArrayFieldItem GetArrayItem<TContentItem>(this TContentItem contentItem, string name, int index)
        where TContentItem : IContentElement
    {
        ArrayField arrayField = contentItem.GetField<ArrayField>(name);

        return arrayField.Items[index];
    }

    public static TContentItem AddArrayItem<TContentItem>(this TContentItem contentItem, string name, ISchemaElement schema, Action<ArrayFieldItem> action)
        where TContentItem : IContentElement
    {
        ArrayFieldOptions options = schema.GetArrayFieldOptions(name);
        ArrayFieldItem item = options.CreateArrayItem();
        
        action(item);

        return AddArrayItem(contentItem, name, item);
    }

    public static TContentItem AddArrayItem<TContentItem>(this TContentItem contentItem, string name, ArrayFieldItem item)
        where TContentItem : IContentElement
    {
        ArrayField arrayField = contentItem.GetField<ArrayField>(name);
        
        arrayField.Items.Add(item);

        return contentItem;
    }

    public static TContentItem RemoveArrayItem<TContentItem>(this TContentItem contentItem, string name, int index)
        where TContentItem : IContentElement
    {
        ArrayField arrayField = contentItem.GetField<ArrayField>(name);

        arrayField.Items.RemoveAt(index);

        return contentItem;
    }
}
