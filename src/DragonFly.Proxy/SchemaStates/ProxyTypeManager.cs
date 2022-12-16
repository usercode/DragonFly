// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Proxy;

public class ProxyTypeManager
{
    private static readonly ProxyTypeManager _default = new ProxyTypeManager();

    public static ProxyTypeManager Default => _default;


    private IDictionary<Type, ContentSchema> _schemaByType = new Dictionary<Type, ContentSchema>();
    private IDictionary<ContentSchema, Type> _schemaBySchema = new Dictionary<ContentSchema, Type>();

    internal void Add(Type type, ContentSchema schema)
    {
        _schemaByType[type] = schema;
        _schemaBySchema[schema] = type;
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
        where TContentModel : IContentModel
    {
        return GetSchemaByType(typeof(TContentModel));
    }

    public TContentModel Create<TContentModel>()
        where TContentModel : IContentModel
    {
        ContentSchema schema = GetSchema<TContentModel>();

        ContentItem content = schema.CreateContent();

        return content.ToModel<TContentModel>();
    }
}
