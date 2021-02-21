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
        public static string GetString(this IContentItem contentItem, string name)
        {
            StringField field = contentItem.GetField<StringField>(name);

            return field.Value;
        }

        public static TContentItem SetString<TContentItem>(this TContentItem contentItem, string name, string value)
            where TContentItem : IContentItem
        {
            StringField field = contentItem.GetField<StringField>(name);

            field.Value = value;

            return contentItem;
        }

        public static string GetSlug(this IContentItem contentItem, string name)
        {
            SlugField field = contentItem.GetField<SlugField>(name);

            return field.Value;
        }

        public static TContentItem SetSlug<TContentItem>(this TContentItem contentItem, string name, string value)
            where TContentItem : IContentItem
        {
            SlugField field = contentItem.GetField<SlugField>(name);

            field.Value = value;

            return contentItem;
        }

        public static string GetTextArea(this IContentItem contentItem, string name)
        {
            TextAreaField field = contentItem.GetField<TextAreaField>(name);

            return field.Value;
        }

        public static TContentItem SetTextArea<TContentItem>(this TContentItem contentItem, string name, string value)
            where TContentItem : IContentItem
        {
            TextAreaField field = contentItem.GetField<TextAreaField>(name);

            field.Value = value;

            return contentItem;
        }

        public static bool? GetBool(this IContentItem contentItem, string name)
        {
            BoolField field = contentItem.GetField<BoolField>(name);

            return field.Value;
        }

        public static TContentItem SetBool<TContentItem>(this TContentItem contentItem, string name, bool? value)
            where TContentItem : IContentItem
        {
            BoolField field = contentItem.GetField<BoolField>(name);

            field.Value = value;

            return contentItem;
        }

        public static double? GetFloat(this IContentItem contentItem, string name)
        {
            FloatField field = contentItem.GetField<FloatField>(name);

            return field.Value;
        }

        public static TContentItem SetFloat<TContentItem>(this TContentItem contentItem, string name, double? value)
            where TContentItem : IContentItem
        {
            FloatField field = contentItem.GetField<FloatField>(name);

            field.Value = value;

            return contentItem;
        }

        public static long? GetInteger(this IContentItem contentItem, string name)
        {
            IntegerField field = contentItem.GetField<IntegerField>(name);

            return field.Value;
        }

        public static TContentItem SetInteger<TContentItem>(this TContentItem contentItem, string name, long? value)
            where TContentItem : IContentItem
        {
            IntegerField field = contentItem.GetField<IntegerField>(name);

            field.Value = value;

            return contentItem;
        }

        public static DateTime? GetDate(this IContentItem contentItem, string name)
        {
            DateField field = contentItem.GetField<DateField>(name);

            return field.Value;
        }

        public static TContentItem SetDate<TContentItem>(this TContentItem contentItem, string name, DateTime? value)
            where TContentItem : IContentItem
        {
            DateField field = contentItem.GetField<DateField>(name);

            field.Value = value;

            return contentItem;
        }

        public static ContentItem GetReference(this IContentItem contentItem, string name)
        {
            ReferenceField field = contentItem.GetField<ReferenceField>(name);

            return field.ContentItem;
        }

        public static TContentItem SetReference<TContentItem>(this TContentItem contentItem, string name, ContentItem reference)
            where TContentItem : IContentItem
        {
            ReferenceField field = contentItem.GetField<ReferenceField>(name);

            field.ContentItem = reference;

            return contentItem;
        }

        public static Asset GetAsset(this IContentItem contentItem, string name)
        {
            AssetField field = contentItem.GetField<AssetField>(name);

            return field.Asset;
        }

        public static TContentItem SetAsset<TContentItem>(this TContentItem contentItem, string name, Asset asset)
            where TContentItem : IContentItem
        {
            AssetField field = contentItem.GetField<AssetField>(name);

            field.Asset = asset;

            return contentItem;
        }
    }
}
