using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.ContentTypes;
using DragonFly.Data.Content;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Data.Models
{
    public static class MongoDbExtensions
    {
        public static void FromBsonValue(this BsonValue bsonValue, string fieldName, IContentItem contentItem, IContentSchema schema)
        {
            if (contentItem.Fields.TryGetValue(fieldName, out ContentField contentField))
            {
                if (bsonValue is BsonString bsonString)
                {
                    ((SingleValueContentField<string>)contentField).Value = bsonString.Value;
                }
                else if (bsonValue is BsonBinaryData bsonBinary && bsonBinary.IsGuid)
                {
                    if (contentField is AssetField assetField)
                    {
                        assetField.Asset = ContentItemProxy.CreateAsset(bsonBinary.ToGuid());
                    }
                    else
                    {
                        ((SingleValueContentField<Guid?>)contentField).Value = bsonBinary.ToGuid();
                    }
                }
                else if (bsonValue is BsonDateTime bsonDate)
                {
                    if (contentField is SingleValueContentField<DateTime?> dateField)
                    {
                        dateField.Value = bsonDate.ToNullableLocalTime();
                    }
                }
                else if (bsonValue is BsonInt32 bsonInt)
                {
                    if (contentField is SingleValueContentField<int?> intField)
                    {
                        intField.Value = bsonInt.Value;
                    }
                }
                else if (bsonValue is BsonInt64 bsonLong)
                {
                    ((SingleValueContentField<long?>)contentField).Value = bsonLong.Value;
                }
                else if (bsonValue is BsonDouble bsonDouble)
                {
                    ((SingleValueContentField<double?>)contentField).Value = bsonDouble.Value;
                }
                else if (bsonValue is BsonBoolean bsonBoolean)
                {
                    ((SingleValueContentField<bool?>)contentField).Value = bsonBoolean.Value;
                }
                else if (bsonValue is BsonArray bsonArray)
                {
                    if (contentField is ArrayField arrayField)
                    {
                        if (schema.Fields.TryGetValue(fieldName, out ContentSchemaField definition))
                        {
                            ArrayFieldOptions arrayOptions = definition.Options as ArrayFieldOptions;

                            foreach (BsonDocument item in bsonArray)
                            {
                                ArrayFieldItem arrayFieldItem = arrayOptions.CreateItem();

                                foreach (BsonElement subitem in item)
                                {
                                    FromBsonValue(subitem.Value, subitem.Name, arrayFieldItem, arrayOptions);
                                }

                                arrayField.Items.Add(arrayFieldItem);
                            }
                        }
                    }
                }                
                else if (bsonValue is BsonDocument bsonDocument)
                {
                    if (contentField is ReferenceField referenceField)
                    {
                        if (bsonDocument[ReferenceField.IdField] != BsonNull.Value)
                        {
                            Guid targetId = bsonDocument[ReferenceField.IdField].AsGuid;
                            string targetType = bsonDocument[ReferenceField.SchemaField].AsString;

                            referenceField.ContentItem = ContentItemProxy.CreateContentItem(targetId, targetType);
                        }
                    }                   
                    else
                    {
                        ContentField c = (ContentField)BsonSerializer.Deserialize(bsonDocument, contentField.GetType());
                        contentItem.Fields[fieldName] = c;
                    }
                }
                else
                {

                }
            }
        }

        public static BsonValue ToBsonValue(this ContentField field)
        {
            BsonValue bsonValue = BsonNull.Value;

            if (field is SingleValueContentField<string> stringField)
            {
                if (stringField.HasValue)
                {
                    bsonValue = new BsonString(stringField.Value);
                }
            }
            else if (field is SingleValueContentField<int?> íntField)
            {
                if (íntField.HasValue)
                {
                    bsonValue = íntField.Value.Value;
                }
            }
            else if (field is SingleValueContentField<long?> longField)
            {
                if (longField.HasValue)
                {
                    bsonValue = new BsonInt64(longField.Value.Value);
                }
            }
            else if (field is SingleValueContentField<double?> doubleField)
            {
                if (doubleField.HasValue)
                {
                    bsonValue = new BsonDouble(doubleField.Value.Value);
                }
            }
            else if (field is SingleValueContentField<bool?> boolField)
            {
                if (boolField.HasValue)
                {
                    bsonValue = new BsonBoolean(boolField.Value.Value);
                }
            }
            else if (field is SingleValueContentField<DateTime?> dateField)
            {
                if (dateField.HasValue)
                {
                    bsonValue = new BsonDateTime(dateField.Value.Value);
                }
            }
            else if (field is SingleValueContentField<Guid?> guidField)
            {
                if (guidField.HasValue)
                {
                    bsonValue = new BsonBinaryData(guidField.Value.Value, GuidRepresentation.Standard);
                }
            }
            else if (field is ReferenceField referenceField)
            {
                if (referenceField.ContentItem != null)
                {
                    BsonDocument doc = new BsonDocument();

                    doc.Add(ReferenceField.IdField, new BsonBinaryData(referenceField.ContentItem.Id, GuidRepresentation.Standard));
                    doc.Add(ReferenceField.SchemaField, referenceField.ContentItem.Schema.Name);

                    bsonValue = doc;
                }
            }
            else if (field is AssetField assetField)
            {
                if(assetField.Asset != null)
                {
                    bsonValue = new BsonBinaryData(assetField.Asset.Id, GuidRepresentation.Standard);
                }
            }
            else if (field is ArrayField arrayField)
            {
                BsonArray bsonArray = new BsonArray();

                foreach (ArrayFieldItem item in arrayField.Items)
                {
                    BsonDocument doc = new BsonDocument();

                    foreach (var f in item.Fields)
                    {
                        doc.Add(f.Key, f.Value.ToBsonValue());
                    }

                    if (doc.ElementCount > 0)
                    {
                        bsonArray.Add(doc);
                    }
                }

                bsonValue = bsonArray;
            }
            else
            {
                bsonValue = field.ToBsonDocument(field.GetType());
            }

            return bsonValue;
        }
    }
}
