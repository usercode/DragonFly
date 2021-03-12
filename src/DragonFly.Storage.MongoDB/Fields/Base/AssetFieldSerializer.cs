using DragonFly.Content;
using DragonFly.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class AssetFieldSerializer : FieldSerializer<AssetField>
    {
        public override void Read(ContentSchemaField definition, AssetField contentField, BsonValue bsonValue)
        {
            if (bsonValue is BsonBinaryData bsonBinary && bsonBinary.IsGuid)
            {
                contentField.Asset = ContentItemProxy.CreateAsset(bsonBinary.ToGuid());
            }
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
}
