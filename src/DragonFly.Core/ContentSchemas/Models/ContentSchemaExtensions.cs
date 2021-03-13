using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    public static class ContentSchemaExtensions
    {
        public static IContentSchema AddField(this IContentSchema schema, string name, Type fieldType, ContentFieldOptions options, int? sortkey = null)
        {
            schema.Fields.Add(name, new ContentSchemaField(ContentFieldManager.Default.GetContentFieldName(fieldType), options) { SortKey = sortkey ?? 0, Options = options });

            return schema;
        }

        public static IContentSchema AddField<TField>(this IContentSchema schema, string name, ContentFieldOptions options, int? sortkey = null)
            where TField : ContentField
        {
            schema.Fields.Add(name, new ContentSchemaField(ContentFieldManager.Default.GetContentFieldName<TField>(), options) { SortKey = sortkey ?? 0, Options = options });

            return schema;
        }

        public static TContentSchema RemoveField<TContentSchema>(this TContentSchema schema, string name)
            where TContentSchema : IContentSchema
        {
            schema.Fields.Remove(name);

            return schema;
        }

        public static TContentSchema AddSlug<TContentSchema>(this TContentSchema schema, string name, Action<SlugFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            SlugFieldOptions options = new SlugFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<SlugField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddString<TContentSchema>(this TContentSchema schema, string name, Action<StringFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            StringFieldOptions options = new StringFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<StringField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddHtml<TContentSchema>(this TContentSchema schema, string name, Action<HtmlFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            HtmlFieldOptions options = new HtmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<HtmlField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddXml<TContentSchema>(this TContentSchema schema, string name, Action<XmlFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            XmlFieldOptions options = new XmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<XmlField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddTextArea<TContentSchema>(this TContentSchema schema, string name, Action<TextAreaFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            TextAreaFieldOptions options = new TextAreaFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<TextAreaField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddDate<TContentSchema>(this TContentSchema schema, string name, Action<DateFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            DateFieldOptions options = new DateFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<DateField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddFloat<TContentSchema>(this TContentSchema schema, string name, Action<FloatFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            FloatFieldOptions options = new FloatFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<FloatField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddInteger<TContentSchema>(this TContentSchema schema, string name, Action<IntegerFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            IntegerFieldOptions options = new IntegerFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<IntegerField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddBool<TContentSchema>(this TContentSchema schema, string name, Action<BoolFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            BoolFieldOptions options = new BoolFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<BoolField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddAsset<TContentSchema>(this TContentSchema schema, string name, Action<AssetFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            AssetFieldOptions options = new AssetFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<AssetField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddReference<TContentSchema>(this TContentSchema schema, string name, Action<ReferenceFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            ReferenceFieldOptions options = new ReferenceFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ReferenceField>(name, options, null);

            return schema;
        }

        public static TContentSchema AddArray<TContentSchema>(this TContentSchema schema, string name, Action<ArrayFieldOptions>? configOptions = null)
            where TContentSchema : IContentSchema
        {
            ArrayFieldOptions options = new ArrayFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ArrayField>(name, options, null);

            return schema;
        }

        public static ArrayFieldOptions GetArrayFieldOptions(this IContentSchema schema, string name)
        {
            return schema.Fields[name].GetArrayFieldOptions();
        }
    }
}
