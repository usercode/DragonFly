// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;

namespace DragonFly.MongoDB;

/// <summary>
/// AssetMongoFieldSerializer
/// </summary>
public class AssetMongoFieldSerializer : MongoFieldSerializer<AssetField>
{
    public override AssetField Read(SchemaField schemaField, BsonValue bsonValue)
    {
        AssetField contentField = new AssetField();

        if (bsonValue is BsonBinaryData bsonBinary && bsonBinary.IsGuid)
        {
            contentField.Asset = ProxyFactory.CreateAsset(bsonBinary.ToGuid(GuidRepresentation.CSharpLegacy));
        }

        return contentField;
    }

    public override BsonValue Write(AssetField contentField)
    {
        if (contentField.Asset != null)
        {
            return new BsonBinaryData(contentField.Asset.Id, GuidRepresentation.CSharpLegacy);
        }
        else
        {
            return BsonNull.Value;
        }
    }
}
