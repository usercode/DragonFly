// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// MongoQueryManager
/// </summary>
public sealed class MongoQueryManager
{
    public static MongoQueryManager Default { get; } = new MongoQueryManager();

    private IDictionary<Type, IFieldQueryAction> _fields = new Dictionary<Type, IFieldQueryAction>();

    public void Add(Type fieldType, IFieldQueryAction queryConverter)
    {
        _fields.Add(fieldType, queryConverter);
    }

    public void Add<TQuery, TQueryAction>()
        where TQuery : FieldQuery
        where TQueryAction : FieldQueryAction<TQuery>, new()
    {
        Add(typeof(TQuery), new TQueryAction());
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
