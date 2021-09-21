using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class BoolFieldSerializer : FieldSerializer<BoolField>
    {
        public override void Read(SchemaField schemaField, BoolField contentField, BsonValue bsonvalue)
        {
            contentField.Value = (bool?)bsonvalue;
        }

        public override BsonValue Write(BoolField contentField)
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
