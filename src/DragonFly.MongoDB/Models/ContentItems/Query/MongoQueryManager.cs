// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoQueryManager
/// </summary>
public sealed class MongoQueryManager
{
    public static MongoQueryManager Default { get; } = new MongoQueryManager();

    private IDictionary<Type, IFieldQueryAction> _fields;

    public MongoQueryManager()
    {
        _fields = new Dictionary<Type, IFieldQueryAction>();
    }

    public void Register(Type fieldType, IFieldQueryAction queryConverter)
    {
        _fields.Add(fieldType, queryConverter);
    }

    public void Register<TQuery, TQueryConverter>()
        where TQuery : FieldQuery
        where TQueryConverter : FieldQueryAction<TQuery>, new()
    {
        Register(typeof(TQuery), new TQueryConverter());
    }

    public IFieldQueryAction GetByType(Type fieldType)
    {
        if (_fields.TryGetValue(fieldType, out IFieldQueryAction? queryConverter))
        {
            return queryConverter;
        }

        throw new Exception($"Field serializer not found: {fieldType.Name}");
    }
}
