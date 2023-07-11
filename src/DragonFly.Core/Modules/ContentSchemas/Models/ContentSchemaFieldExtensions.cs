// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentSchemaFieldExtensions
/// </summary>
public static class ContentSchemaFieldExtensions
{
    public static TContentSchema AddSlug<TContentSchema>(this TContentSchema schema, string name, Action<SlugFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        SlugFieldOptions options = new SlugFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(SlugField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddString<TContentSchema>(this TContentSchema schema, string name, Action<StringFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        StringFieldOptions options = new StringFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(StringField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddHtml<TContentSchema>(this TContentSchema schema, string name, Action<HtmlFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        HtmlFieldOptions options = new HtmlFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(HtmlField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddXml<TContentSchema>(this TContentSchema schema, string name, Action<XmlFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        XmlFieldOptions options = new XmlFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(XmlField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddTextArea<TContentSchema>(this TContentSchema schema, string name, Action<TextFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        TextFieldOptions options = new TextFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(TextField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddDate<TContentSchema>(this TContentSchema schema, string name, Action<DateTimeFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        DateTimeFieldOptions options = new DateTimeFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(DateTimeField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddFloat<TContentSchema>(this TContentSchema schema, string name, Action<FloatFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        FloatFieldOptions options = new FloatFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(FloatField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddInteger<TContentSchema>(this TContentSchema schema, string name, Action<IntegerFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        IntegerFieldOptions options = new IntegerFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(IntegerField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddBool<TContentSchema>(this TContentSchema schema, string name, Action<BoolFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        BoolFieldOptions options = new BoolFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(BoolField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddGeolocation<TContentSchema>(this TContentSchema schema, string name, Action<GeolocationFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        GeolocationFieldOptions options = new GeolocationFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(GeolocationField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddAsset<TContentSchema>(this TContentSchema schema, string name, Action<AssetFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        AssetFieldOptions options = new AssetFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(AssetField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddReference<TContentSchema>(this TContentSchema schema, ContentSchema schemaReference, Action<ReferenceFieldOptions>? configOptions = null, int sortkey = 0)
       where TContentSchema : ISchemaElement
    {
        return AddReference(schema, schemaReference.Name, configOptions, sortkey);
    }

    public static TContentSchema AddReference<TContentSchema>(this TContentSchema schema, string name, Action<ReferenceFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        ReferenceFieldOptions options = new ReferenceFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(ReferenceField), options, sortkey);

        return schema;
    }

    public static TContentSchema AddArray<TContentSchema>(this TContentSchema schema, string name, Action<ArrayFieldOptions>? configOptions = null, int sortkey = 0)
        where TContentSchema : ISchemaElement
    {
        ArrayFieldOptions options = new ArrayFieldOptions();

        configOptions?.Invoke(options);

        schema.AddField(name, typeof(ArrayField), options, sortkey);

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
