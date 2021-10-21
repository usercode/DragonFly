using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
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

        private IDictionary<Type, Type> _optionsByField;
        private IDictionary<Type, Type> _queryByField;
        private IDictionary<string, Type> _fieldByName;

        public event ContentItemAddedHandler? Added;

        private ContentFieldManager()
        {
            _optionsByField = new Dictionary<Type, Type>();
            _queryByField = new Dictionary<Type, Type>();
            _fieldByName = new Dictionary<string, Type>();
        }

        public void Register<TField>()
            where TField : ContentField
        {
            Type contentFieldType = typeof(TField);

            //name
            _fieldByName[contentFieldType.Name] = contentFieldType;

            //options
            FieldOptionsAttribute? fieldOptionsAttribute = contentFieldType.GetCustomAttribute<FieldOptionsAttribute>();

            if (fieldOptionsAttribute != null)
            {
                _optionsByField[typeof(TField)] = fieldOptionsAttribute.OptionsType;
            }

            //query
            FieldQueryAttribute? fieldQueryAttribute = contentFieldType.GetCustomAttribute<FieldQueryAttribute>();

            if (fieldQueryAttribute != null)
            {
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
            where T : ContentField
        {
            return GetContentFieldName(typeof(T));
        }

        public string GetContentFieldName(Type type)
        {
            return type.Name;
        }

        public Type GetContentFieldType(string fieldType)
        {
            if (fieldType == null)
            {
                throw new ArgumentNullException(nameof(fieldType));
            }

            return _fieldByName[fieldType];
        }

        public ContentField CreateField<T>()
            where T : ContentField
        {
            return CreateField(typeof(T));
        }

        public IEnumerable<ContentField> CreateContentFields()
        {
            return GetAllFieldTypes().Select(x => CreateField(x)).ToList();
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

        public ContentField CreateField(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);

            return CreateField(fieldType);
        }

        public Type? GetOptionsType(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);

            if (_optionsByField.TryGetValue(fieldType, out Type? type))
            {
                return type;
            }

            return null;
        }

        public Type GetQueryType(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);
            Type queryType = _queryByField[fieldType];

            return queryType;
        }

        public FieldQuery CreateQuery(string? fieldName)
        {
            Type type = GetQueryType(fieldName);

            FieldQuery? instance = (FieldQuery?)Activator.CreateInstance(type);

            if (instance == null)
            {
                throw new Exception();
            }

            return instance;
        }

        public ContentFieldOptions? CreateOptions(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);
            ContentFieldOptions? options = CreateOptions(fieldType);

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
}
