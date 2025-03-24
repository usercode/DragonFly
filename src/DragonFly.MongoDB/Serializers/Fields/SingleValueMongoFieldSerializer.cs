// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;

namespace DragonFly.Storage.MongoDB.Fields;

/// <summary>
/// SingleValueMongoFieldSerializer
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public class SingleValueMongoFieldSerializer<TContentField> : MongoFieldSerializer<TContentField>
    where TContentField : ContentField, ISingleValueField, new()
{
    public override TContentField Read(SchemaField schemaField,  BsonValue bsonValue)
    {
        TContentField contentField = new TContentField();

        if (contentField is SingleValueField<string> stringField)
        {
            stringField.Value = (string?)bsonValue;
        }
        else if (contentField is SingleValueField<bool?> boolField)
        {
            boolField.Value = (bool?)bsonValue;
        }
        else if (contentField is SingleValueField<byte?> byteField)
        {
            byteField.Value = (byte?)bsonValue;
        }
        else if (contentField is SingleValueField<short?> shortField)
        {
            shortField.Value = (short?)bsonValue;
        }
        else if (contentField is SingleValueField<int?> intField)
        {
            intField.Value = (int?)bsonValue;
        }
        else if (contentField is SingleValueField<long?> longField)
        {
            longField.Value = (long?)bsonValue;
        }
        else if (contentField is SingleValueField<float?> floatField)
        {
            floatField.Value = (float?)bsonValue;
        }
        else if (contentField is SingleValueField<double?> doubleField)
        {
            doubleField.Value = (double?)bsonValue;
        }
        else if (contentField is SingleValueField<DateTime?> dateTimeField)
        {
            dateTimeField.Value = (DateTime?)bsonValue;
        }
        else if (contentField is SingleValueField<Guid?> guidField)
        {
            guidField.Value = (Guid?)bsonValue;
        }

        return contentField;
    }

    public override BsonValue Write(TContentField contentField)
    {
        if (contentField.HasValue)
        {
            return BsonValue.Create(contentField.Value);
        }
        else
        {
            return BsonNull.Value;
        }
    }
}
