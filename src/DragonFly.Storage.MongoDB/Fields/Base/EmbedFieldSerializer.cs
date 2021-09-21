using DragonFly.Content;
using DragonFly.Data;
using DragonFly.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class EmbedFieldSerializer : FieldSerializer<EmbedField>
    {
        public override void Read(SchemaField schemaField, EmbedField contentField, BsonValue bsonValue)
        {
            string schemaName = bsonValue[ReferenceField.SchemaField].AsString;

            ContentSchema schema = MongoStorage.Default.GetContentSchemaAsync(schemaName).GetAwaiter().GetResult();

            contentField.ContentEmbedded = schema.CreateContentEmbedded();

            if (bsonValue[nameof(IContentElement.Fields)] is BsonDocument bsonDocument)
            {
                foreach (BsonElement field in bsonDocument)
                {
                    field.Value.ToModelValue(field.Name, contentField.ContentEmbedded, schema);
                }
            }
        }

        public override BsonValue Write(EmbedField contentField)
        {
            if (contentField.ContentEmbedded != null)
            {
                BsonDocument doc = new BsonDocument();
                doc.Add(ReferenceField.SchemaField, contentField.ContentEmbedded.Schema.Name);
                
                BsonDocument fields = new BsonDocument();

                foreach (var f in contentField.ContentEmbedded.Fields)
                {
                    fields.Add(f.Key, f.Value.ToBsonValue());
                }

                doc.Add(nameof(IContentElement.Fields), fields);

                return doc;
            }
            else
            {
                return BsonNull.Value;
            }
        }
    }
}
