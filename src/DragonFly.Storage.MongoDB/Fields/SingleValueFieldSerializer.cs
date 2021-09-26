﻿using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Storage.Abstractions;

namespace DragonFly.Storage.MongoDB.Fields
{
    /// <summary>
    /// SingleValueFieldSerializer
    /// </summary>
    /// <typeparam name="TTField"></typeparam>
    public class SingleValueFieldSerializer<TTField> : FieldSerializer<TTField>
        where TTField : ContentField, ISingleValueContentField, new()
    {
        public override TTField Read(SchemaField schemaField,  BsonValue bsonValue)
        {
            TTField contentField = new TTField();

            if (contentField is SingleValueContentField<string> stringField)
            {
                stringField.Value = (string?)bsonValue;
            }
            else if (contentField is SingleValueContentField<bool?> boolField)
            {
                boolField.Value = (bool?)bsonValue;
            }
            else if (contentField is SingleValueContentField<byte?> byteField)
            {
                byteField.Value = (byte?)bsonValue;
            }
            else if (contentField is SingleValueContentField<short?> shortField)
            {
                shortField.Value = (short?)bsonValue;
            }
            else if (contentField is SingleValueContentField<int?> intField)
            {
                intField.Value = (int?)bsonValue;
            }
            else if (contentField is SingleValueContentField<long?> longField)
            {
                longField.Value = (long?)bsonValue;
            }
            else if (contentField is SingleValueContentField<float?> floatField)
            {
                floatField.Value = (float?)bsonValue;
            }
            else if (contentField is SingleValueContentField<double?> doubleField)
            {
                doubleField.Value = (double?)bsonValue;
            }
            else if (contentField is SingleValueContentField<DateTime?> dateField)
            {
                dateField.Value = (DateTime?)bsonValue;
            }
            if (contentField is SingleValueContentField<Guid?> guidField)
            {
                guidField.Value = (Guid?)bsonValue;
            }

            return contentField;
        }

        public override BsonValue Write(TTField contentField)
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
}
