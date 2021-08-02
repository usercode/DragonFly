using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    public static class ContentSchemaExtensions
    {
        public static ISchemaElement AddField(this ISchemaElement schema, string name, Type fieldType, ContentFieldOptions? options = null, int sortkey = 0)
        {
            if (options == null)
            {
                options = ContentFieldManager.Default.CreateOptions(name);
            }

            schema.Fields.Add(name, new ContentSchemaField(ContentFieldManager.Default.GetContentFieldName(fieldType), options) { SortKey = sortkey, Options = options });

            return schema;
        }

        public static ISchemaElement AddField<TField>(this ISchemaElement schema, string name, ContentFieldOptions? options = null, int sortkey = 0)
            where TField : ContentField
        {
            return AddField(schema, name, typeof(TField), options, sortkey);
        }

        public static TContentSchema RemoveField<TContentSchema>(this TContentSchema schema, string name)
            where TContentSchema : ISchemaElement
        {
            schema.Fields.Remove(name);

            return schema;
        }

        public static TContentSchema AddSlug<TContentSchema>(this TContentSchema schema, string name, Action<SlugFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            SlugFieldOptions options = new SlugFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<SlugField>(name, options);

            return schema;
        }

        public static TContentSchema AddString<TContentSchema>(this TContentSchema schema, string name, Action<StringFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            StringFieldOptions options = new StringFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<StringField>(name, options);

            return schema;
        }

        public static TContentSchema AddHtml<TContentSchema>(this TContentSchema schema, string name, Action<HtmlFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            HtmlFieldOptions options = new HtmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<HtmlField>(name, options);

            return schema;
        }

        public static TContentSchema AddXml<TContentSchema>(this TContentSchema schema, string name, Action<XmlFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            XmlFieldOptions options = new XmlFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<XmlField>(name, options);

            return schema;
        }

        public static TContentSchema AddTextArea<TContentSchema>(this TContentSchema schema, string name, Action<TextAreaFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            TextAreaFieldOptions options = new TextAreaFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<TextAreaField>(name, options);

            return schema;
        }

        public static TContentSchema AddDate<TContentSchema>(this TContentSchema schema, string name, Action<DateFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            DateFieldOptions options = new DateFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<DateField>(name, options);

            return schema;
        }

        public static TContentSchema AddFloat<TContentSchema>(this TContentSchema schema, string name, Action<FloatFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            FloatFieldOptions options = new FloatFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<FloatField>(name, options);

            return schema;
        }

        public static TContentSchema AddInteger<TContentSchema>(this TContentSchema schema, string name, Action<IntegerFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            IntegerFieldOptions options = new IntegerFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<IntegerField>(name, options);

            return schema;
        }

        public static TContentSchema AddBool<TContentSchema>(this TContentSchema schema, string name, Action<BoolFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            BoolFieldOptions options = new BoolFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<BoolField>(name, options);

            return schema;
        }

        public static TContentSchema AddAsset<TContentSchema>(this TContentSchema schema, string name, Action<AssetFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            AssetFieldOptions options = new AssetFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<AssetField>(name, options);

            return schema;
        }

        public static TContentSchema AddReference<TContentSchema>(this TContentSchema schema, ContentSchema schemaReference, Action<ReferenceFieldOptions>? configOptions = null)
           where TContentSchema : ISchemaElement
        {
            return AddReference(schema, schemaReference.Name, configOptions);
        }

        public static TContentSchema AddReference<TContentSchema>(this TContentSchema schema, string name, Action<ReferenceFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            ReferenceFieldOptions options = new ReferenceFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ReferenceField>(name, options);

            return schema;
        }

        public static TContentSchema AddArray<TContentSchema>(this TContentSchema schema, string name, Action<ArrayFieldOptions>? configOptions = null)
            where TContentSchema : ISchemaElement
        {
            ArrayFieldOptions options = new ArrayFieldOptions();

            configOptions?.Invoke(options);

            schema.AddField<ArrayField>(name, options);

            return schema;
        }

        public static ArrayFieldOptions GetArrayFieldOptions(this ISchemaElement schema, string name)
        {
            return schema.Fields[name].GetArrayFieldOptions();
        }
    }
}
