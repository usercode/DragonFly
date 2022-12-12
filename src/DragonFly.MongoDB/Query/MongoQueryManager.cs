// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly.MongoDB.Query;

/// <summary>
/// MongoQueryManager
/// </summary>
public class MongoQueryManager
{
    private static MongoQueryManager? _default;

    public static MongoQueryManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new MongoQueryManager();

                _default.Register<BoolFieldQuery, BoolFieldQueryAction>();
                _default.Register<StringFieldQuery, StringFieldQueryAction>();
                _default.Register<SlugFieldQuery, SlugFieldQueryAction>();
                _default.Register<IntegerFieldQuery, IntegerFieldQueryAction>();
                _default.Register<ReferenceFieldQuery, ReferenceFieldQueryAction>();
                _default.Register<AssetFieldQuery, AssetFieldQueryAction>();
            }

            return _default;
        }
    }

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
