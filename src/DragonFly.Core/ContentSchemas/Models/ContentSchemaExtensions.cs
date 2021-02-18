using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    public static class ContentSchemaExtensions
    {
        public static IContentSchema AddField<TField>(this IContentSchema schema, string name, int? sortkey = null, ContentFieldOptions options = null)
            where TField : ContentField
        {
            schema.Fields.Add(name, new ContentFieldDefinition() { SortKey = sortkey ?? 0, FieldType = ContentFieldManager.Default.GetContentFieldName<TField>(), Options = options });

            return schema;
        }

        public static IContentSchema AddSlug(this IContentSchema schema, string name, Action<SlugFieldOptions> configOptions = null)
        {
            SlugFieldOptions options = new SlugFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<SlugField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddString(this IContentSchema schema, string name, Action<StringFieldOptions> configOptions = null)
        {
            StringFieldOptions options = new StringFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<StringField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddHtml(this IContentSchema schema, string name, Action<HtmlFieldOptions> configOptions = null)
        {
            HtmlFieldOptions options = new HtmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<HtmlField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddXml(this IContentSchema schema, string name, Action<XmlFieldOptions> configOptions = null)
        {
            XmlFieldOptions options = new XmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<XmlField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddTextArea(this IContentSchema schema, string name, Action<TextAreaFieldOptions> configOptions = null)
        {
            TextAreaFieldOptions options = new TextAreaFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<TextAreaField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddDate(this IContentSchema schema, string name, Action<DateFieldOptions> configOptions = null)
        {
            DateFieldOptions options = new DateFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<DateField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddFloat(this IContentSchema schema, string name, Action<FloatFieldOptions> configOptions = null)
        {
            FloatFieldOptions options = new FloatFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<FloatField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddBool(this IContentSchema schema, string name, Action<BoolFieldOptions> configOptions = null)
        {
            BoolFieldOptions options = new BoolFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<BoolField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddAsset(this IContentSchema schema, string name, Action<AssetFieldOptions> configOptions = null)
        {
            AssetFieldOptions options = new AssetFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<AssetField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddReference(this IContentSchema schema, string name, Action<ReferenceFieldOptions> configOptions = null)
        {
            ReferenceFieldOptions options = new ReferenceFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ReferenceField>(name, null, options);

            return schema;
        }

        public static IContentSchema AddArray(this IContentSchema schema, string name, Action<ArrayFieldOptions> configOptions = null)
        {
            ArrayFieldOptions options = new ArrayFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ArrayField>(name, null, options);

            return schema;
        }
    }
}
