// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;
using DragonFly.MongoDB.Proxies;

namespace DragonFly.MongoDB;

public class ReferenceFieldSerializer : FieldSerializer<ReferenceField>
{
    public override ReferenceField Read(SchemaField schemaField, BsonValue bsonValue)
    {
        ReferenceField contentField = new ReferenceField();

        if (bsonValue is BsonDocument bsonDocument)
        {
            if (bsonDocument[ReferenceField.IdField] != BsonNull.Value)
            {
                Guid targetId = bsonDocument[ReferenceField.IdField].AsGuid;
                string targetType = bsonDocument[ReferenceField.SchemaField].AsString;

                contentField.ContentItem = ContentItemProxy.CreateContentItem(targetType, targetId);
            }
        }

        return contentField;
    }

    public override BsonValue Write(ReferenceField contentField)
    {
        if (contentField.ContentItem != null)
        {
            BsonDocument doc = new BsonDocument();

            doc.Add(ReferenceField.IdField, new BsonBinaryData(contentField.ContentItem.Id, GuidRepresentation.Standard));
            doc.Add(ReferenceField.SchemaField, contentField.ContentItem.Schema.Name);

            return doc;
        }
        else
        {
            return BsonNull.Value;
        }
    }
}
