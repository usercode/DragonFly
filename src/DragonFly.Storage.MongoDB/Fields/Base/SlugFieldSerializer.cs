using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class SlugFieldSerializer : FieldSerializer<SlugField>
    {
        public override void Read(ContentSchemaField schemaField, SlugField contentField, BsonValue bsonvalue)
        {
            if (bsonvalue is BsonString bsonString)
            {
                contentField.Value = bsonString.Value;
            }
        }

        public override BsonValue Write(SlugField contentField)
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
