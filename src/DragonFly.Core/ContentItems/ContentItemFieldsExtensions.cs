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

        public static void SetString(this IContentItem contentItem, string name, string value)
        {
            StringField field = contentItem.GetField<StringField>(name);

            field.Value = value;
        }

        public static string GetSlug(this IContentItem contentItem, string name)
        {
            SlugField field = contentItem.GetField<SlugField>(name);

            return field.Value;
        }

        public static void SetSlug(this IContentItem contentItem, string name, string value)
        {
            SlugField field = contentItem.GetField<SlugField>(name);

            field.Value = value;
        }

        public static string GetTextArea(this IContentItem contentItem, string name)
        {
            TextAreaField field = contentItem.GetField<TextAreaField>(name);

            return field.Value;
        }

        public static void SetTextArea(this IContentItem contentItem, string name, string value)
        {
            TextAreaField field = contentItem.GetField<TextAreaField>(name);

            field.Value = value;
        }

        public static bool? GetBool(this IContentItem contentItem, string name)
        {
            BoolField field = contentItem.GetField<BoolField>(name);

            return field.Value;
        }

        public static void SetBool(this IContentItem contentItem, string name, bool? value)
        {
            BoolField field = contentItem.GetField<BoolField>(name);

            field.Value = value;
        }

        public static double? GetFloat(this IContentItem contentItem, string name)
        {
            FloatField field = contentItem.GetField<FloatField>(name);

            return field.Value;
        }

        public static void SetFloat(this IContentItem contentItem, string name, double? value)
        {
            FloatField field = contentItem.GetField<FloatField>(name);

            field.Value = value;
        }

        public static DateTime? GetDate(this IContentItem contentItem, string name)
        {
            DateField field = contentItem.GetField<DateField>(name);

            return field.Value;
        }

        public static void SetDate(this IContentItem contentItem, string name, DateTime? value)
        {
            DateField field = contentItem.GetField<DateField>(name);

            field.Value = value;
        }

        public static ContentItem GetReference(this IContentItem contentItem, string name)
        {
            ReferenceField field = contentItem.GetField<ReferenceField>(name);

            return field.ContentItem;
        }

        public static void SetReference(this IContentItem contentItem, string name, ContentItem reference)
        {
            ReferenceField field = contentItem.GetField<ReferenceField>(name);

            field.ContentItem = reference;
        }
    }
}
