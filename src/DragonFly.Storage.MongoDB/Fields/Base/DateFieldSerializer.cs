using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class DateFieldSerializer : FieldSerializer<DateField>
    {
        public override void Read(ContentSchemaField schemaField, DateField contentField, BsonValue bsonvalue)
        {
            contentField.Value = (DateTime?)bsonvalue;
        }

        public override BsonValue Write(DateField contentField)
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
