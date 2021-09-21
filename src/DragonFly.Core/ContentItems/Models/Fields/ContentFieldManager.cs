using DragonFly.Content;
using DragonFly.Core.ContentItems.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentFieldManager
    /// </summary>
    public class ContentFieldManager
    {
        private IDictionary<Type, Type> _optionsByField;
        private IDictionary<Type, Type> _queryByField;
        private IDictionary<string, Type> _fieldByName;

        private static ContentFieldManager? _default;

        public static ContentFieldManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new ContentFieldManager();

                    _default.RegisterField<ArrayField>();
                    _default.RegisterField<AssetField>();
                    _default.RegisterField<BoolField>();
                    _default.RegisterField<DateField>();
                    _default.RegisterField<EmbedField>();
                    _default.RegisterField<FloatField>();
                    _default.RegisterField<HtmlField>();
                    _default.RegisterField<IntegerField>();
                    _default.RegisterField<ReferenceField>();
                    _default.RegisterField<SlugField>();
                    _default.RegisterField<StringField>();
                    _default.RegisterField<TextAreaField>();
                    _default.RegisterField<XHtmlField>();
                    _default.RegisterField<XmlField>();
                }

                return _default;
            }
        }

        private ContentFieldManager()
        {
            _optionsByField = new Dictionary<Type, Type>();
            _queryByField = new Dictionary<Type, Type>();
            _fieldByName = new Dictionary<string, Type>();
        }

        public void RegisterField<TField>()
            where TField : ContentField, new()
        {
            //name
            _fieldByName.Add(typeof(TField).Name, typeof(TField));

            //options
            FieldOptionsAttribute? fieldOptionsAttribute = typeof(TField).GetCustomAttribute<FieldOptionsAttribute>();

            if (fieldOptionsAttribute != null)
            {
                _optionsByField.Add(typeof(TField), fieldOptionsAttribute.OptionsType);
            }

            //query
            FieldQueryAttribute? fieldQueryAttribute = typeof(TField).GetCustomAttribute<FieldQueryAttribute>();

            if (fieldQueryAttribute != null)
            {
                _queryByField.Add(typeof(TField), fieldQueryAttribute.QueryType);
            }
        }

        public IEnumerable<Type> GetAllOptionTypes()
        {
            return _optionsByField.Values;
        }

        public IEnumerable<Type> GetAllFieldTypes()
        {
            return _optionsByField.Select(x => x.Key).ToList();
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

        public Type GetContentFieldType(string fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            return _fieldByName[fieldName];
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

        public ContentField CreateField(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);

            return CreateField(fieldType);
        }

        public Type GetOptionsType(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);
            Type optionsType = _optionsByField[fieldType];

            return optionsType;
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

        public FieldQueryBase CreateQuery(string? fieldName)
        {
            Type type = GetQueryType(fieldName);

            FieldQueryBase? instance = (FieldQueryBase?)Activator.CreateInstance(type);

            if (instance == null)
            {
                throw new Exception();
            }

            return instance;
        }

        public ContentFieldOptions CreateOptions(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);
            ContentFieldOptions options = CreateOptions(fieldType);

            return options;
        }

        public ContentFieldOptions CreateOptions<TField>()
            where TField : ContentField
        {
            return CreateOptions(typeof(TField));
        }

        public ContentFieldOptions CreateOptions(Type fieldType)
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
                throw new Exception($"Field not found: {fieldType.Name}");
            }
        }
    }
}
