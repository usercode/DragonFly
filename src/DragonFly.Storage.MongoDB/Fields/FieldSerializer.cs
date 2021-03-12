using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields
{
    public abstract class FieldSerializer<TContentField> : IFieldSerializer
        where TContentField : ContentField
    {
        public abstract void Read(ContentSchemaField schemaField, TContentField contentField, BsonValue bsonValue);

        public abstract BsonValue Write(TContentField contentField);

        public BsonValue Write(ContentField contentField)
        {
            return Write((TContentField)contentField);
        }

        void IFieldSerializer.Read(ContentSchemaField definition, ContentField contentField, BsonValue bsonvalue)
        {
            Read(definition, (TContentField)contentField, bsonvalue);
        }
    }
}
