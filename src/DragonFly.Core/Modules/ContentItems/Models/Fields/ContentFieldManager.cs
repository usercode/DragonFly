﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using System.Reflection;

namespace DragonFly;

public delegate void ContentItemAddedHandler(Type contentFieldType, FieldOptionsAttribute? fieldOptionsAttribute, FieldQueryAttribute? fieldQueryAttribute);

/// <summary>
/// ContentFieldManager
/// </summary>
public sealed class ContentFieldManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static ContentFieldManager Default { get; } = new ContentFieldManager();

    private IDictionary<string, Type> _optionsByName;
    private IDictionary<Type, Type> _optionsByField;
    private IDictionary<string, Type> _queryByName;
    private IDictionary<Type, Type> _queryByField;
    private IDictionary<string, Type> _fieldByName;

    public event ContentItemAddedHandler? Added;

    private ContentFieldManager()
    {
        _optionsByName = new Dictionary<string, Type>();
        _optionsByField = new Dictionary<Type, Type>();
        _queryByName = new Dictionary<string, Type>();
        _queryByField = new Dictionary<Type, Type>();
        _fieldByName = new Dictionary<string, Type>();
    }

    public void Add<TField>()
        where TField : ContentField
    {
        Add(typeof(TField));
    }

    public void Add(Type fieldType)
    {
        //name
        _fieldByName[fieldType.Name] = fieldType;

        //options
        FieldOptionsAttribute? fieldOptionsAttribute = fieldType.GetCustomAttribute<FieldOptionsAttribute>();

        if (fieldOptionsAttribute != null)
        {
            _optionsByName[fieldOptionsAttribute.OptionsType.Name] = fieldOptionsAttribute.OptionsType;
            _optionsByField[fieldType] = fieldOptionsAttribute.OptionsType;
        }

        //query
        FieldQueryAttribute? fieldQueryAttribute = fieldType.GetCustomAttribute<FieldQueryAttribute>();

        if (fieldQueryAttribute != null)
        {
            _queryByName[fieldQueryAttribute.QueryType.Name] = fieldQueryAttribute.QueryType;
            _queryByField[fieldType] = fieldQueryAttribute.QueryType;
        }

        Added?.Invoke(fieldType, fieldOptionsAttribute, fieldQueryAttribute);
    }

    public IEnumerable<Type> GetAllOptionsTypes()
    {
        return _optionsByField.Values;
    }

    public IEnumerable<Type> GetAllFieldTypes()
    {
        return _fieldByName.Select(x => x.Value).OrderBy(x => x.Name).ToList();
    }

    public IEnumerable<Type> GetAllQueryTypes()
    {
        return _queryByField.Values;
    }

    public string GetContentFieldName<T>()
        where T : ContentField
    {
        return GetContentFieldName(typeof(T));
    }

    public string GetContentFieldName(Type type)
    {
        return type.Name;
    }

    public Type? GetContentFieldType(string fieldTypeName)
    {
        if (fieldTypeName == null)
        {
            throw new ArgumentNullException(nameof(fieldTypeName));
        }

        if (_fieldByName.TryGetValue(fieldTypeName, out Type? type))
        {
            return type;
        }

        return null;
    }

    public ContentField CreateField<T>()
        where T : ContentField
    {
        return CreateField(typeof(T));
    }

    public ContentField CreateField(Type t)
    {
        ContentField? field = (ContentField?)Activator.CreateInstance(t);

        if (field == null)
        {
            throw new Exception($"Cannot create an instance of field '{t.Name}'");
        }

        return field;
    }

    public ContentField? CreateField(string? fieldName)
    {
        if (fieldName == null)
        {
            throw new ArgumentNullException(nameof(fieldName));
        }

        Type? fieldType = GetContentFieldType(fieldName);

        if (fieldType == null)
        {
            return null;
        }

        return CreateField(fieldType);
    }

    public Type? GetOptionsTypeByName(string name)
    {
        if (_optionsByName.TryGetValue(name, out Type? type))
        {
            return type;
        }

        return null;
    }

    public Type? GetOptionsType(string? fieldName)
    {
        if (fieldName == null)
        {
            throw new ArgumentNullException(nameof(fieldName));
        }

        Type? fieldType = GetContentFieldType(fieldName);

        if (fieldType == null)
        {
            return null;
        }

        if (_optionsByField.TryGetValue(fieldType, out Type? type))
        {
            return type;
        }

        return null;
    }

    public Type? GetQueryType(string? fieldType)
    {
        if (fieldType == null)
        {
            throw new ArgumentNullException(nameof(fieldType));
        }

        Type? type = GetContentFieldType(fieldType);

        if (fieldType == null)
        {
            return null;
        }

        if (_queryByField.TryGetValue(type, out Type? queryType))
        {
            return queryType;
        }

        return null;
    }

    public Type? GetQueryByName(string name)
    {
        if (_queryByName.TryGetValue(name, out Type? type))
        {
            return type;
        }

        return null;
    }

    public FieldQuery? CreateQuery(string? fieldType)
    {
        Type? type = GetQueryType(fieldType);

        if (type == null)
        {
            return null;
        }

        FieldQuery? instance = (FieldQuery?)Activator.CreateInstance(type);

        if (instance == null)
        {
            return null;
        }

        return instance;
    }

    public ContentFieldOptions? CreateOptions(string? fieldType)
    {
        if (fieldType == null)
        {
            throw new ArgumentNullException(nameof(fieldType));
        }

        Type? type = GetContentFieldType(fieldType);

        if (type == null)
        {
            return null;
        }

        ContentFieldOptions? options = CreateOptions(type);

        return options;
    }

    public ContentFieldOptions? CreateOptions<TField>()
        where TField : ContentField
    {
        return CreateOptions(typeof(TField));
    }

    public ContentFieldOptions? CreateOptions(Type fieldType)
    {
        if (_optionsByField.TryGetValue(fieldType, out Type? t))
        {
            ContentFieldOptions? options = (ContentFieldOptions?)Activator.CreateInstance(t);

            if (options == null)
            {
                throw new Exception($"Could not create options for {fieldType.Name}.");
            }

            return options;
        }
        else
        {
            return null;
        }
    }
}