using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content;

public delegate void ContentItemAddedHandler(Type contentFieldType, FieldOptionsAttribute? fieldOptionsAttribute, FieldQueryAttribute? fieldQueryAttribute);

/// <summary>
/// ContentFieldManager
/// </summary>
public class ContentFieldManager
{
    private static ContentFieldManager? _default;

    public static ContentFieldManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new ContentFieldManager();
            }

            return _default;
        }
    }

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
        where TField : IContentField
    {
        Type contentFieldType = typeof(TField);

        //name
        _fieldByName[contentFieldType.Name] = contentFieldType;

        //options
        FieldOptionsAttribute? fieldOptionsAttribute = contentFieldType.GetCustomAttribute<FieldOptionsAttribute>();

        if (fieldOptionsAttribute != null)
        {
            _optionsByName[fieldOptionsAttribute.OptionsType.Name] = fieldOptionsAttribute.OptionsType;
            _optionsByField[typeof(TField)] = fieldOptionsAttribute.OptionsType;
        }

        //query
        FieldQueryAttribute? fieldQueryAttribute = contentFieldType.GetCustomAttribute<FieldQueryAttribute>();

        if (fieldQueryAttribute != null)
        {
            _queryByName[fieldQueryAttribute.QueryType.Name] = fieldQueryAttribute.QueryType;
            _queryByField[contentFieldType] = fieldQueryAttribute.QueryType;
        }

        Added?.Invoke(contentFieldType, fieldOptionsAttribute, fieldQueryAttribute);
    }

    public IEnumerable<Type> GetAllOptionsTypes()
    {
        return _optionsByField.Values;
    }

    public IEnumerable<Type> GetAllFieldTypes()
    {
        return _fieldByName.Select(x => x.Value).ToList();
    }

    public IEnumerable<Type> GetAllQueryTypes()
    {
        return _queryByField.Values;
    }

    public string GetContentFieldName<T>()
        where T : IContentField
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

    public IContentField CreateField<T>()
        where T : IContentField
    {
        return CreateField(typeof(T));
    }

    public IEnumerable<IContentField> CreateContentFields()
    {
        return GetAllFieldTypes().Select(x => CreateField(x)).ToList();
    }

    public IContentField CreateField(Type t)
    {
        IContentField? field = (IContentField?)Activator.CreateInstance(t);

        if (field == null)
        {
            throw new Exception($"Cannot create an instance of field '{t.Name}'");
        }

        return field;
    }

    public IContentField? CreateField(string? fieldName)
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

    public Type? GetQueryType(string? fieldTypeName)
    {
        if (fieldTypeName == null)
        {
            throw new ArgumentNullException(nameof(fieldTypeName));
        }

        Type? fieldType = GetContentFieldType(fieldTypeName);

        if (fieldType == null)
        {
            return null;
        }

        if (_queryByField.TryGetValue(fieldType, out Type? queryType))
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

    public FieldQuery? CreateQuery(string? fieldTypeName)
    {
        Type? type = GetQueryType(fieldTypeName);

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

    public ContentFieldOptions? CreateOptions(string? fieldTypeName)
    {
        if (fieldTypeName == null)
        {
            throw new ArgumentNullException(nameof(fieldTypeName));
        }

        Type? fieldType = GetContentFieldType(fieldTypeName);

        if (fieldType == null)
        {
            return null;
        }

        ContentFieldOptions? options = CreateOptions(fieldType);

        return options;
    }

    public ContentFieldOptions? CreateOptions<TField>()
        where TField : IContentField
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
