﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Proxy;

public sealed class ProxyTypeManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static ProxyTypeManager Default { get; } = new ProxyTypeManager();

    private IDictionary<Type, ContentSchema> _schemaByType = new Dictionary<Type, ContentSchema>();
    private IDictionary<ContentSchema, Type> _schemaBySchema = new Dictionary<ContentSchema, Type>();

    internal void Add(Type type, ContentSchema schema)
    {
        _schemaByType[type] = schema;
        _schemaBySchema[schema] = type;
    }

    internal void Add<T>(ContentSchema schema)
    {
        Add(typeof(T), schema);
    }

    public ContentSchema GetSchemaByType(Type type)
    {
        return _schemaByType[type];
    }

    public Type GetTypeBySchema(ContentSchema schema)
    {
        return _schemaBySchema[schema];
    }

    public ContentSchema GetSchema<TContentModel>()
        where TContentModel : class, IContentModel
    {
        return GetSchemaByType(typeof(TContentModel));
    }

    public TContentModel Create<TContentModel>()
        where TContentModel : class, IContentModel, new()
    {
        ContentSchema schema = GetSchema<TContentModel>();

        ContentItem content = schema.CreateContent();

        return content.ToModel<TContentModel>();
    }
}
