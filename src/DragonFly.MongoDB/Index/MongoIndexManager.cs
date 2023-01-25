// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace DragonFly.MongoDB;

/// <summary>
/// FieldIndexManager
/// </summary>
public sealed class MongoIndexManager
{
    private static MongoIndexManager? _default;

    public static MongoIndexManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new MongoIndexManager();

                _default.Register<StringField>();
                _default.Register<SlugField>();
                _default.Register<BoolField>();
                _default.Register<IntegerField>();
                _default.Register<FloatField>();
                _default.Register<DateTimeField>();
                //_default.Register<AssetField>(null);
                //_default.Register<ReferenceField>(ReferenceField.IdField);
            }

            return _default;
        }
    }

    public MongoIndexManager()
    {
        Fields = new Dictionary<Type, FieldIndex>();
    }

    public IDictionary<Type, FieldIndex> Fields { get; set; }

    public void Register<TField>(string? name, bool unique = false)
        where TField : ContentField
    {
        Fields[typeof(TField)] = new FieldIndex(name, unique);
    }

    public void Register<TField>(bool unique = false)
        where TField : ContentField, ISingleValueField
    {
        Register<TField>(null, unique);
    }

    public bool TryGetByType(Type fieldType, [NotNullWhen(true)] out FieldIndex? fieldIndex)
    {
        if (Fields.TryGetValue(fieldType, out var result))
        {
            fieldIndex = result;

            return true;
        }

        fieldIndex = null;

        return false;
    }
}
