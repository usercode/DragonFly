using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Storage.Abstractions;
using MongoDB.Bson.Serialization;

namespace DragonFly.Storage.MongoDB.Fields
{
    public class DefaultFieldSerializer<TTField> : FieldSerializer<TTField>
        where TTField : ContentField, new()
    {
        public override TTField Read(SchemaField schemaField, BsonValue bsonValue)
        {
            if (bsonValue is BsonDocument bsonDocument)
            {
                return BsonSerializer.Deserialize<TTField>(bsonDocument);
            }

            return new TTField();
        }

        public override BsonValue Write(TTField contentField)
        {
            return contentField.ToBsonDocument(contentField.GetType());
        }
    }
}
