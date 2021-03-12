using DragonFly.Content;
using DragonFly.Storage.MongoDB.Fields.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields
{
    public class MongoFieldManager
    {
        private static MongoFieldManager _default;

        public static MongoFieldManager Default
        {
            get
            {
                if(_default == null)
                {
                    _default = new MongoFieldManager();

                    _default.Register<ArrayField, ArrayFieldSerializer>();
                    _default.Register<AssetField, AssetFieldSerializer>();
                    _default.Register<BoolField, BoolFieldSerializer>();
                    _default.Register<DateField, DateFieldSerializer>();
                    _default.Register<EmbedField, EmbedFieldSerializer>();
                    _default.Register<FloatField, FloatFieldSerializer>();
                    _default.Register<IntegerField, IntegerFieldSerializer>();
                    _default.Register<ReferenceField, ReferenceFieldSerializer>();
                    _default.Register<StringField, StringFieldSerializer>();
                    _default.Register<SlugField, SlugFieldSerializer>();
                    _default.Register<TextAreaField, TextAreaFieldSerializer>();
                    _default.Register<XHtmlField, XHtmlFieldSerializer>();
                }

                return _default;
            }
        }

        private IDictionary<Type, IFieldSerializer> _fields;

        public MongoFieldManager()
        {
            _fields = new Dictionary<Type, IFieldSerializer>();
        }

        public void Register(Type fieldType, IFieldSerializer fieldSerializer)
        {
            _fields.Add(fieldType, fieldSerializer);
        }

        public void Register<TContentField, TSerializer>()
            where TContentField : ContentField
            where TSerializer : FieldSerializer<TContentField>, new()
        {
            Register(typeof(TContentField), new TSerializer());
        }

        public IFieldSerializer GetByType(Type fieldType)
        {
            if (_fields.TryGetValue(fieldType, out IFieldSerializer fieldSerializer))
            {
                return fieldSerializer;
            }

            throw new Exception($"Field serializer not found: {fieldType.Name}");
        }
    }
}
