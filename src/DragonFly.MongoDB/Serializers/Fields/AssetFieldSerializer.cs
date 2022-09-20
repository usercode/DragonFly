using DragonFly.Content;
using DragonFly.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Storage.Abstractions;
using DragonFly.MongoDB.Proxies;

namespace DragonFly.MongoDB;

public class AssetFieldSerializer : FieldSerializer<AssetField>
{
    public override AssetField Read(SchemaField schemaField, BsonValue bsonValue)
    {
        AssetField contentField = new AssetField();

        if (bsonValue is BsonBinaryData bsonBinary && bsonBinary.IsGuid)
        {
            contentField.Asset = ContentItemProxy.CreateAsset(bsonBinary.ToGuid());
        }

        return contentField;
    }

    public override BsonValue Write(AssetField contentField)
    {
        if (contentField.Asset != null)
        {
            return new BsonBinaryData(contentField.Asset.Id, GuidRepresentation.Standard);
        }
        else
        {
            return BsonNull.Value;
        }
    }
}
