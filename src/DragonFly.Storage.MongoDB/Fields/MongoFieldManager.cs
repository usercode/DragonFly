using DragonFly.Content;
using DragonFly.Storage.Abstractions;
using DragonFly.Storage.MongoDB.Fields.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields
{
    /// <summary>
    /// MongoFieldManager
    /// </summary>
    public class MongoFieldManager
    {
        private static MongoFieldManager? _default;

        public static MongoFieldManager Default
        {
            get
            {
                if(_default == null)
                {
                    _default = new MongoFieldManager();

                    _default.Register<ArrayFieldSerializer>();
                    _default.Register<AssetFieldSerializer>();
                    _default.Register<EmbedFieldSerializer>();
                    _default.Register<ReferenceFieldSerializer>();               

                    _default.Register<SingleValueFieldSerializer<StringField>>();
                    _default.Register<SingleValueFieldSerializer<SlugField>>();
                    _default.Register<SingleValueFieldSerializer<IntegerField>>();
                    _default.Register<SingleValueFieldSerializer<TextAreaField>>();
                    _default.Register<SingleValueFieldSerializer<HtmlField>>();
                    _default.Register<SingleValueFieldSerializer<XHtmlField>>();
                    _default.Register<SingleValueFieldSerializer<DateField>>();
                }

                return _default;
            }
        }

        private IDictionary<Type, IFieldSerializer> _fields;

        public MongoFieldManager()
        {
            _fields = new Dictionary<Type, IFieldSerializer>();
        }

        public void Register(IFieldSerializer fieldSerializer)
        {
            if (_fields.TryAdd(fieldSerializer.FieldType, fieldSerializer) == false)
            {
                _fields[fieldSerializer.FieldType] = fieldSerializer;
            }
        }

        public void Register<TSerializer>()
            where TSerializer : IFieldSerializer, new()
        {
            TSerializer serializer = new TSerializer();

            Register(serializer);
        }

        public IFieldSerializer GetByType(Type contentFieldType)
        {
            if (_fields.TryGetValue(contentFieldType, out IFieldSerializer? fieldSerializer))
            {
                return fieldSerializer;
            }

            if (contentFieldType.GetInterfaces().Any(x => x == typeof(ISingleValueContentField)))
            {
                //create SingleValueFieldSerializer
                fieldSerializer = (IFieldSerializer?)Activator.CreateInstance(typeof(SingleValueFieldSerializer<>).MakeGenericType(contentFieldType));

                if (fieldSerializer == null)
                {
                    throw new Exception($"Could not create FieldSerializer for {contentFieldType.Name}");
                }

                _fields.Add(contentFieldType, fieldSerializer);

                return fieldSerializer;
            }

            throw new Exception($"Field serializer not found: {contentFieldType.Name}");
        }
    }
}
