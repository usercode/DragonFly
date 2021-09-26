using DragonFly.Content;
using DragonFly.Storage.Abstractions;
using MongoDB.Bson;
using System;
using System.Text.Json;

namespace DragonFly.Fields.BlockField.Storage
{
    /// <summary>
    /// BlockFieldSerializer
    /// </summary>
    public class BlockFieldSerializer : FieldSerializer<BlockField>
    {
        public override BlockField Read(SchemaField schemaField, BsonValue bsonValue)
        {
            BlockField contentField = new BlockField();

            contentField.Value = (string?)bsonValue;

            return contentField;
        }

        public override BsonValue Write(BlockField contentField)
        {
            if (contentField.HasValue)
            {
                return contentField.Value;
            }
            else
            {
                return BsonNull.Value;
            }
        }
    }
}