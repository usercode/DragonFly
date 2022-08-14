using DragonFly.Content;
using DragonFly.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Storage.Abstractions;

namespace DragonFly.Storage.MongoDB.Fields.Base;

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
