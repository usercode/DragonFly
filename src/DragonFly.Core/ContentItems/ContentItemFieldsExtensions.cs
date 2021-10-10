using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentItemFieldsExtensions
    /// </summary>
    public static class ContentItemFieldsExtensions
    {
        public static string? GetString(this IContentElement contentItem, string name)
        {
            StringField field = contentItem.GetField<StringField>(name);

            return field.Value;
        }

        public static TContentItem SetString<TContentItem>(this TContentItem contentItem, string name, string? value)
            where TContentItem : IContentElement
        {
            StringField field = contentItem.GetField<StringField>(name);

            field.Value = value;

            return contentItem;
        }

        public static string? GetSlug(this IContentElement contentItem, string name)
        {
            SlugField field = contentItem.GetField<SlugField>(name);

            return field.Value;
        }

        public static TContentItem SetSlug<TContentItem>(this TContentItem contentItem, string name, string? value)
            where TContentItem : IContentElement
        {
            SlugField field = contentItem.GetField<SlugField>(name);

            field.Value = value;

            return contentItem;
        }

        public static string? GetTextArea(this IContentElement contentItem, string name)
        {
            TextAreaField field = contentItem.GetField<TextAreaField>(name);

            return field.Value;
        }

        public static TContentItem SetTextArea<TContentItem>(this TContentItem contentItem, string name, string? value)
            where TContentItem : IContentElement
        {
            TextAreaField field = contentItem.GetField<TextAreaField>(name);

            field.Value = value;

            return contentItem;
        }

        public static bool? GetBool(this IContentElement contentItem, string name)
        {
            BoolField field = contentItem.GetField<BoolField>(name);

            return field.Value;
        }

        public static TContentItem SetBool<TContentItem>(this TContentItem contentItem, string name, bool? value)
            where TContentItem : IContentElement
        {
            BoolField field = contentItem.GetField<BoolField>(name);

            field.Value = value;

            return contentItem;
        }

        public static double? GetFloat(this IContentElement contentItem, string name)
        {
            FloatField field = contentItem.GetField<FloatField>(name);

            return field.Value;
        }

        public static TContentItem SetFloat<TContentItem>(this TContentItem contentItem, string name, double? value)
            where TContentItem : IContentElement
        {
            FloatField field = contentItem.GetField<FloatField>(name);

            field.Value = value;

            return contentItem;
        }

        public static long? GetInteger(this IContentElement contentItem, string name)
        {
            IntegerField field = contentItem.GetField<IntegerField>(name);

            return field.Value;
        }

        public static TContentItem SetInteger<TContentItem>(this TContentItem contentItem, string name, long? value)
            where TContentItem : IContentElement
        {
            IntegerField field = contentItem.GetField<IntegerField>(name);

            field.Value = value;

            return contentItem;
        }

        public static DateTime? GetDate(this IContentElement contentItem, string name)
        {
            DateField field = contentItem.GetField<DateField>(name);

            return field.Value;
        }

        public static TContentItem SetDate<TContentItem>(this TContentItem contentItem, string name, DateTime? value)
            where TContentItem : IContentElement
        {
            DateField field = contentItem.GetField<DateField>(name);

            field.Value = value;

            return contentItem;
        }

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
            ArrayFieldItem item = options.CreateArrayField();
            
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
}
