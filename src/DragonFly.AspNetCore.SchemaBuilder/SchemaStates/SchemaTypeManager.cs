using System;
using System.Collections.Generic;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.SchemaStates;

public class SchemaTypeManager
{
    private static readonly SchemaTypeManager _default = new SchemaTypeManager();

    public static SchemaTypeManager Default => _default;
    
    
    private IDictionary<Type, ContentSchema> _schema = new Dictionary<Type, ContentSchema>();

    internal void Add(Type type, ContentSchema schema)
    {
        _schema[type] = schema;
    }

    public ContentSchema Get(Type type)
    {
        return _schema[type];
    }

    public ContentSchema Get<T>()
    {
        return Get(typeof(T));
    }

}
