using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class FloatFieldSerializer : FieldSerializer<FloatField>
    {
        public override void Read(SchemaField schemaField, FloatField contentField, BsonValue bsonvalue)
        {
            contentField.Value = (double?)bsonvalue;
        }

        public override BsonValue Write(FloatField contentField)
        {
            if (contentField.HasValue)
            {
                return contentField.Value;
            }
            else
            {
                return BsonNull.Value;
            }
        }
    }
}
