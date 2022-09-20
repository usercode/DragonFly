using DragonFly.Storage.Abstractions;
using DragonFly.Storage.MongoDB.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB;

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

                _default.RegisterField<ArrayFieldSerializer>();
                _default.RegisterField<AssetFieldSerializer>();
                _default.RegisterField<EmbedFieldSerializer>();
                _default.RegisterField<ReferenceFieldSerializer>();

                _default.RegisterField<SingleValueFieldSerializer<StringField>>();
                _default.RegisterField<SingleValueFieldSerializer<SlugField>>();
                _default.RegisterField<SingleValueFieldSerializer<IntegerField>>();
                _default.RegisterField<SingleValueFieldSerializer<FloatField>>();
                _default.RegisterField<SingleValueFieldSerializer<TextField>>();
                _default.RegisterField<SingleValueFieldSerializer<HtmlField>>();
                _default.RegisterField<SingleValueFieldSerializer<XHtmlField>>();
                _default.RegisterField<SingleValueFieldSerializer<XmlField>>();
                _default.RegisterField<SingleValueFieldSerializer<DateTimeField>>();
            }

            return _default;
        }
    }

    private IDictionary<Type, IFieldSerializer> _fields;

    public MongoFieldManager()
    {
        _fields = new Dictionary<Type, IFieldSerializer>();
    }

    public void RegisterField(IFieldSerializer fieldSerializer)
    {
        if (_fields.TryAdd(fieldSerializer.FieldType, fieldSerializer) == false)
        {
            _fields[fieldSerializer.FieldType] = fieldSerializer;
        }
    }

    public void RegisterField<TSerializer>()
        where TSerializer : IFieldSerializer, new()
    {
        TSerializer serializer = new TSerializer();

        RegisterField(serializer);
    }

    public IFieldSerializer GetByFieldType(Type contentFieldType)
    {
        if (_fields.TryGetValue(contentFieldType, out IFieldSerializer? fieldSerializer))
        {
            return fieldSerializer;
        }

        //Build SingleValueSerializer?
        if (contentFieldType.GetInterfaces().Any(x => x == typeof(ISingleValueField)))
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
        else //Build DefaultFieldSerializer
        {
            fieldSerializer = (IFieldSerializer?)Activator.CreateInstance(typeof(DefaultFieldSerializer<>).MakeGenericType(contentFieldType));

            if (fieldSerializer == null)
            {
                throw new Exception($"Could not create FieldSerializer for {contentFieldType.Name}");
            }

            _fields.Add(contentFieldType, fieldSerializer);

            return fieldSerializer;
        }
    }
}
