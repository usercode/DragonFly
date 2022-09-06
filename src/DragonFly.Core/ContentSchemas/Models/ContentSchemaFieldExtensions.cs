using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// ContentSchemaFieldExtensions
/// </summary>
public static class ContentSchemaFieldExtensions
{
    public static TContentSchema AddSlug<TContentSchema>(this TContentSchema schema, string name, Action<SlugFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        SlugFieldOptions options = new SlugFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<SlugField>(name, options);

        return schema;
    }

    public static TContentSchema AddString<TContentSchema>(this TContentSchema schema, string name, Action<StringFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        StringFieldOptions options = new StringFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<StringField>(name, options);

        return schema;
    }

    public static TContentSchema AddHtml<TContentSchema>(this TContentSchema schema, string name, Action<HtmlFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        HtmlFieldOptions options = new HtmlFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<HtmlField>(name, options);

        return schema;
    }

    public static TContentSchema AddXml<TContentSchema>(this TContentSchema schema, string name, Action<XmlFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        XmlFieldOptions options = new XmlFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<XmlField>(name, options);

        return schema;
    }

    public static TContentSchema AddTextArea<TContentSchema>(this TContentSchema schema, string name, Action<TextFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        TextFieldOptions options = new TextFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<TextField>(name, options);

        return schema;
    }

    public static TContentSchema AddDate<TContentSchema>(this TContentSchema schema, string name, Action<DateTimeFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        DateTimeFieldOptions options = new DateTimeFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<DateTimeField>(name, options);

        return schema;
    }

    public static TContentSchema AddFloat<TContentSchema>(this TContentSchema schema, string name, Action<FloatFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        FloatFieldOptions options = new FloatFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<FloatField>(name, options);

        return schema;
    }

    public static TContentSchema AddInteger<TContentSchema>(this TContentSchema schema, string name, Action<IntegerFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        IntegerFieldOptions options = new IntegerFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<IntegerField>(name, options);

        return schema;
    }

    public static TContentSchema AddBool<TContentSchema>(this TContentSchema schema, string name, Action<BoolFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        BoolFieldOptions options = new BoolFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<BoolField>(name, options);

        return schema;
    }

    public static TContentSchema AddAsset<TContentSchema>(this TContentSchema schema, string name, Action<AssetFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        AssetFieldOptions options = new AssetFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<AssetField>(name, options);

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

        schema.AddOrUpdateField<ReferenceField>(name, options);

        return schema;
    }

    public static TContentSchema AddArray<TContentSchema>(this TContentSchema schema, string name, Action<ArrayFieldOptions>? configOptions = null)
        where TContentSchema : ISchemaElement
    {
        ArrayFieldOptions options = new ArrayFieldOptions();

        configOptions?.Invoke(options);

        schema.AddOrUpdateField<ArrayField>(name, options);

        return schema;
    }

    public static ArrayFieldOptions GetArrayFieldOptions(this ISchemaElement schema, string name)
    {
        return schema.Fields[name].GetArrayFieldOptions();
    }

    public static ArrayFieldOptions GetArrayFieldOptions(this SchemaField schemaField)
    {
        ArrayFieldOptions? options = (ArrayFieldOptions?)schemaField.Options;

        if (options == null)
        {
            throw new Exception();
        }

        return options;
    }
}
