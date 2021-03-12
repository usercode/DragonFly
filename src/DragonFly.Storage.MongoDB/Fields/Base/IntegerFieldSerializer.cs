using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class IntegerFieldSerializer : FieldSerializer<IntegerField>
    {
        public override void Read(ContentSchemaField schemaField, IntegerField contentField, BsonValue bsonvalue)
        {
            contentField.Value = (long?)bsonvalue;
        }

        public override BsonValue Write(IntegerField contentField)
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
