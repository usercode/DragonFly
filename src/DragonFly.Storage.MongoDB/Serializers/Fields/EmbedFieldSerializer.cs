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
using DragonFly.Storage.Abstractions;

namespace DragonFly.Storage.MongoDB.Fields.Base;

/// <summary>
/// EmbedFieldSerializer
/// </summary>
public class EmbedFieldSerializer : FieldSerializer<EmbedField>
{
    public override EmbedField Read(SchemaField schemaField, BsonValue bsonValue)
    {
        EmbedField contentField = new EmbedField();

        string schemaName = bsonValue[ReferenceField.SchemaField].AsString;

        ContentSchema schema = MongoStorage.Default.GetSchemaAsync(schemaName).GetAwaiter().GetResult();

        contentField.ContentEmbedded = schema.CreateContentEmbedded();

        if (bsonValue[nameof(IContentElement.Fields)] is BsonDocument bsonDocument)
        {
            foreach (BsonElement field in bsonDocument)
            {
                field.Value.ToModelValue(field.Name, contentField.ContentEmbedded, schema);
            }
        }

        return contentField;
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
