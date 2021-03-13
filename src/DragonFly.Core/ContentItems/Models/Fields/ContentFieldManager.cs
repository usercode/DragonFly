using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public class ContentFieldManager
    {
        private IDictionary<Type, Type> _byField;
        private IDictionary<string, Type> _byFieldName;

        private static ContentFieldManager? _default;

        public static ContentFieldManager Default
        {
            get
            {
                if(_default == null)
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
            _byField = new Dictionary<Type, Type>();
            _byFieldName = new Dictionary<string, Type>();
        }

        public void RegisterField<TField>()
            where TField : ContentField, new()
        {
            FieldOptionsAttribute? fieldOptions = typeof(TField).GetCustomAttribute<FieldOptionsAttribute>();

            if (fieldOptions == null)
            {
                throw new Exception($"FieldOptionsAttribute does not exisits: {typeof(TField).Name}");
            }

            _byField.Add(typeof(TField), fieldOptions.OptionsType);
            _byFieldName.Add(typeof(TField).Name, typeof(TField));
        }

        public IEnumerable<Type> GetAllOptionTypes()
        {
            return _byField.Values;
        }

        public IEnumerable<Type> GetAllFieldTypes()
        {
            return _byField.Select(x => x.Key).ToList();
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

            return _byFieldName[fieldName];
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

            Type t = GetContentFieldType(fieldName);

            return CreateField(t);
        }

        public Type GetOptionsType(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = _byFieldName[fieldName];

            return _byField[fieldType];
        }

        public ContentFieldOptions CreateOptions(string? fieldName)
        {
            if (fieldName == null)
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            Type fieldType = GetContentFieldType(fieldName);

            return CreateOptions(fieldType);
        }

        public ContentFieldOptions CreateOptions<TField>()
            where TField : ContentField
        {
            return CreateOptions(typeof(TField));
        }

        public ContentFieldOptions CreateOptions(Type fieldType)
        {
            if (_byField.TryGetValue(fieldType, out Type? t))
            {
                ContentFieldOptions? options = (ContentFieldOptions?)Activator.CreateInstance(t);

                if(options == null)
                {
                    throw new Exception();
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
