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

        public static TContentSchema AddSlug<TContentSchema>(this TContentSchema schema, string name, Action<SlugFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            SlugFieldOptions options = new SlugFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<SlugField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddString<TContentSchema>(this TContentSchema schema, string name, Action<StringFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            StringFieldOptions options = new StringFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<StringField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddHtml<TContentSchema>(this TContentSchema schema, string name, Action<HtmlFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            HtmlFieldOptions options = new HtmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<HtmlField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddXml<TContentSchema>(this TContentSchema schema, string name, Action<XmlFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            XmlFieldOptions options = new XmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<XmlField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddTextArea<TContentSchema>(this TContentSchema schema, string name, Action<TextAreaFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            TextAreaFieldOptions options = new TextAreaFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<TextAreaField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddDate<TContentSchema>(this TContentSchema schema, string name, Action<DateFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            DateFieldOptions options = new DateFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<DateField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddFloat<TContentSchema>(this TContentSchema schema, string name, Action<FloatFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            FloatFieldOptions options = new FloatFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<FloatField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddBool<TContentSchema>(this TContentSchema schema, string name, Action<BoolFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            BoolFieldOptions options = new BoolFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<BoolField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddAsset<TContentSchema>(this TContentSchema schema, string name, Action<AssetFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            AssetFieldOptions options = new AssetFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<AssetField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddReference<TContentSchema>(this TContentSchema schema, string name, Action<ReferenceFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            ReferenceFieldOptions options = new ReferenceFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ReferenceField>(name, null, options);

            return schema;
        }

        public static TContentSchema AddArray<TContentSchema>(this TContentSchema schema, string name, Action<ArrayFieldOptions> configOptions = null)
            where TContentSchema : IContentSchema
        {
            ArrayFieldOptions options = new ArrayFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ArrayField>(name, null, options);

            return schema;
        }
    }
}
