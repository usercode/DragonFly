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
                    _default.Register<BoolFieldSerializer>();
                    _default.Register<DateFieldSerializer>();
                    _default.Register<EmbedFieldSerializer>();
                    _default.Register<FloatFieldSerializer>();
                    _default.Register<IntegerFieldSerializer>();
                    _default.Register<ReferenceFieldSerializer>();
                    _default.Register<StringFieldSerializer>();
                    _default.Register<SlugFieldSerializer>();
                    _default.Register<TextAreaFieldSerializer>();
                    _default.Register<XHtmlFieldSerializer>();
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
            _fields.Add(fieldSerializer.FieldType, fieldSerializer);
        }

        public void Register<TSerializer>()
            where TSerializer : IFieldSerializer, new()
        {
            TSerializer serializer = new TSerializer();

            Register(serializer);
        }

        public IFieldSerializer GetByType(Type fieldType)
        {
            if (_fields.TryGetValue(fieldType, out IFieldSerializer? fieldSerializer))
            {
                return fieldSerializer;
            }

            throw new Exception($"Field serializer not found: {fieldType.Name}");
        }
    }
}
