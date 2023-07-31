// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;

namespace DragonFly.MongoDB;

/// <summary>
/// ComponentMongoFieldSerializer
/// </summary>
public class ComponentMongoFieldSerializer : MongoFieldSerializer<ComponentField>
{
    public override ComponentField Read(SchemaField schemaField, BsonValue bsonValue)
    {
        ComponentField contentField = new ComponentField();

        string schemaName = bsonValue[ReferenceField.SchemaField].AsString;

        ContentSchema? schema = MongoStorage.Default.GetSchemaAsync(schemaName).GetAwaiter().GetResult();

        contentField.ContentComponent = schema.CreateEmbeddedContent();

        if (bsonValue[nameof(IContentElement.Fields)] is BsonDocument bsonDocument)
        {
            foreach (BsonElement field in bsonDocument)
            {
                field.Value.ToModelValue(field.Name, contentField.ContentComponent, schema);
            }
        }

        return contentField;
    }

    public override BsonValue Write(ComponentField contentField)
    {
        if (contentField.ContentComponent != null)
        {
            BsonDocument doc = new BsonDocument();
            doc.Add(ReferenceField.SchemaField, contentField.ContentComponent.Schema.Name);
            
            BsonDocument fields = new BsonDocument();

            foreach (var f in contentField.ContentComponent.Fields)
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
