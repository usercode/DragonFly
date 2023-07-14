// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public delegate void FieldAddedHandler(FieldFactory fieldFactory);

/// <summary>
/// FieldManager
/// </summary>
public sealed class FieldManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static FieldManager Default { get; } = new FieldManager();

    private readonly IDictionary<string, FieldFactory> _optionsByName = new Dictionary<string, FieldFactory>();
    private readonly IDictionary<string, FieldFactory> _queryByName = new Dictionary<string, FieldFactory>();
    private readonly IDictionary<string, FieldFactory> _fieldByName = new Dictionary<string, FieldFactory>();
    private readonly IDictionary<Type, FieldFactory> _fieldByType = new Dictionary<Type, FieldFactory>();

    public event FieldAddedHandler? Added;

    public void Add<TField>()
        where TField : IContentField
    {
        FieldFactory factory = TField.Factory;

        //name
        _fieldByName[factory.FieldName] = factory;
        _fieldByType[factory.FieldType] = factory;

        //options
        if (factory.OptionsType != null)
        {
            _optionsByName[factory.OptionsType.Name] = factory;
        }

        //query
        if (factory.QueryType != null)
        {
            _queryByName[factory.QueryType.Name] = factory;
        }

        Added?.Invoke(factory);
    }

    public IEnumerable<Type> GetAllFieldTypes()
    {
        return _fieldByType.Keys;
    }

    public IEnumerable<Type> GetAllOptionsTypes()
    {
        return _optionsByName.Values
                                .Select(x => x.OptionsType)
                                .Cast<Type>()
                                .ToArray();
    }

    public IEnumerable<Type> GetAllQueryTypes()
    {
        return _queryByName.Values
                                .Select(x => x.QueryType)
                                .Cast<Type>()
                                .ToArray();
    }

    public string GetFieldName<T>()
        where T : ContentField
    {
        return GetFieldName(typeof(T));
    }

    public string GetFieldName(Type type)
    {
        if (_fieldByType.TryGetValue(type, out FieldFactory? factory))
        {
            return factory.FieldName;
        }

        throw new Exception($"The field '{type.Name}' wasn't found.");
    }

    public Type GetFieldType(string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (_fieldByName.TryGetValue(fieldName, out FieldFactory? factory))
        {
            return factory.FieldType;
        }

        throw new Exception($"The field '{fieldName}' wasn't found.");
    }

    public ContentField CreateField<T>()
        where T : ContentField
    {
        return CreateField(typeof(T));
    }

    public ContentField CreateField(Type fieldType)
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        if (_fieldByType.TryGetValue(fieldType, out FieldFactory? factory) == false)
        {
            throw new Exception($"Cannot create an instance of field '{fieldType.Name}'");
        }

        ContentField field = factory.CreateField();

        return field;
    }

    public ContentField CreateField(string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (_fieldByName.TryGetValue(fieldName, out FieldFactory? factory) == false)
        {
            throw new Exception($"Cannot create an instance of field '{fieldName}'");
        }

        ContentField field = factory.CreateField();

        return field;
    }

    public Type? GetOptionsTypeByName(string name)
    {
        if (_optionsByName.TryGetValue(name, out FieldFactory? factory))
        {
            return factory.OptionsType;
        }

        return null;
    }

    public Type? GetOptionsType(string? fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (_fieldByName.TryGetValue(fieldName, out FieldFactory? factory))
        {
            return factory.OptionsType;
        }

        return null;
    }

    public Type? GetQueryType(string? fieldType)
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        if (_fieldByName.TryGetValue(fieldType, out FieldFactory? factory))
        {
            return factory.QueryType;
        }

        return null;
    }

    public Type? GetQueryByName(string name)
    {
        if (_queryByName.TryGetValue(name, out FieldFactory? factory))
        {
            return factory.QueryType;
        }

        return null;
    }

    public FieldQuery? CreateQuery(string? fieldType)
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        if (_fieldByName.TryGetValue(fieldType, out FieldFactory? factory))
        {
            FieldQuery? query = factory.CreateQuery();

            return query;
        }

        return null;
    }

    public FieldOptions? CreateOptions(string? fieldType)
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        if (_fieldByName.TryGetValue(fieldType, out FieldFactory? factory))
        {
            FieldOptions? options = factory.CreateOptions();

            return options;
        }

        return null;
    }

    public FieldOptions? CreateOptions<TField>()
        where TField : ContentField
    {
        return CreateOptions(typeof(TField));
    }

    public FieldOptions? CreateOptions(Type fieldType)
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        if (_fieldByType.TryGetValue(fieldType, out FieldFactory? factory))
        {
            FieldOptions? options = factory.CreateOptions();

            return options;
        }

        return null;
    }
}
