// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;

namespace DragonFly.MongoDB;

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

        contentField.ContentEmbedded = schema.CreateEmbeddedContent();

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
