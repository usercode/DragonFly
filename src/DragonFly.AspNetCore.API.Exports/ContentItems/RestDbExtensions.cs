using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Client;
using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.Data.Models;
using DragonFly.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Models
{
    public static class RestDbExtensions
    {
        public static void FromRestValue(this JToken bsonValue, string fieldName, IContentElement contentItem, ISchemaElement schema)
        {
            if (contentItem.Fields.TryGetValue(fieldName, out ContentField contentField))
            {
                switch(bsonValue.Type)
                {
                    case JTokenType.Null:
                        break;

                    case JTokenType.String:
                        if (contentField is SingleValueContentField<string> stringField)
                        {
                            stringField.Value = bsonValue.Value<string>();
                        }
                        else if (contentField is SingleValueContentField<Guid?> guidField)
                        {
                            guidField.Value = Guid.Parse(bsonValue.Value<string>());
                        }
                        break;

                    case JTokenType.Integer:
                        if (contentField is SingleValueContentField<short?> shortField)
                        {
                            shortField.Value = bsonValue.Value<short?>();
                        }
                        else if (contentField is SingleValueContentField<int?> intField)
                        {
                            intField.Value = bsonValue.Value<int?>();
                        }
                        else if (contentField is SingleValueContentField<long?> longField)
                        {
                            longField.Value = bsonValue.Value<long?>();
                        }
                        break;

                    case JTokenType.Float:
                        ((SingleValueContentField<double?>)contentField).Value = bsonValue.Value<double>();
                        break;

                    case JTokenType.Boolean:
                        ((SingleValueContentField<bool?>)contentField).Value = bsonValue.Value<bool>();
                        break;

                    //case JTokenType.Guid:
                    //    ((SingleContentField<Guid?>)contentField).Value = bsonValue.Value<Guid>();
                    //    break;

                    case JTokenType.Array:
                        if (contentField is ArrayField arrayField)
                        {
                            if (schema.Fields.TryGetValue(fieldName, out SchemaField definition))
                            {
                                ArrayFieldOptions arrayOptions = definition.Options as ArrayFieldOptions;

                                foreach (JObject item in bsonValue)
                                {
                                    ArrayFieldItem arrayFieldItem = arrayOptions.CreateArrayField();

                                    foreach (var subitem in item)
                                    {
                                        FromRestValue(subitem.Value, subitem.Key, arrayFieldItem, arrayOptions);
                                    }

                                    arrayField.Items.Add(arrayFieldItem);
                                }
                            }
                        }
                        break;

                    case JTokenType.Object:
                        if (contentField is ReferenceField referenceField)
                        {
                            if (bsonValue.HasValues)
                            {
                                RestContentItem restContentItem = bsonValue.ToObject<RestContentItem>(NewtonJsonExtensions.CreateSerializer());
                                referenceField.ContentItem = restContentItem.ToModel();
                            }
                        }
                        else if (contentField is EmbedField embedField)
                        {
                            RestContentEmbedded restContentItem = bsonValue.ToObject<RestContentEmbedded>(NewtonJsonExtensions.CreateSerializer());
                            embedField.ContentEmbedded = restContentItem.ToModel();
                        }
                        else if (contentField is AssetField assetField)
                        {
                            if (bsonValue.HasValues)
                            {
                                RestAsset restAsset = bsonValue.ToObject<RestAsset>();
                                assetField.Asset = restAsset.ToModel();
                            }
                        }
                        else
                        {
                            ContentField c = (ContentField)bsonValue.ToObject(contentField.GetType(), NewtonJsonExtensions.CreateSerializer());

                            contentItem.Fields[fieldName] = c;
                        }
                        break;
                }               
            }
        }

        public static JToken ToRestValue(this ContentField field)
        {
            return ToRestValue(field, true);
        }

        public static JToken ToRestValue(this ContentField field, bool includeNavigationProperties)
        {
            JToken bsonValue = JValue.CreateNull();

            if (field is ISingleValueContentField singleValueField)
            {
                if (singleValueField.HasValue)
                {
                    bsonValue = new JValue(singleValueField.Value);
                }
            }
            else if (field is ReferenceField referenceField)
            {
                if (referenceField.ContentItem != null)
                {
                    if (includeNavigationProperties == true)
                    {
                        bsonValue = JObject.FromObject(referenceField.ContentItem.ToRest(true, false), NewtonJsonExtensions.CreateSerializer());
                    }
                }
            }
            else if (field is EmbedField embedField)
            {
                if (embedField.ContentEmbedded != null)
                {
                    bsonValue = JObject.FromObject(embedField.ContentEmbedded.ToRest(), NewtonJsonExtensions.CreateSerializer());
                }
            }
            else if (field is AssetField assetField)
            {
                if (assetField.Asset != null)
                {
                    bsonValue = JObject.FromObject(assetField.Asset.ToRest());
                }
            }
            else if (field is ArrayField arrayField)
            {
                JArray bsonArray = new JArray();

                foreach (ArrayFieldItem item in arrayField.Items)
                {
                    JObject doc = new JObject();

                    foreach (var f in item.Fields)
                    {
                        doc.Add(f.Key, f.Value.ToRestValue(includeNavigationProperties));
                    }

                    if (doc.Count > 0)
                    {
                        bsonArray.Add(doc);
                    }
                }

                if (bsonArray.Count > 0)
                {
                    bsonValue = bsonArray;
                }

                //arrayField.Items
            }
            else
            {
                //bsonValue = field.ToBsonDocument(field.GetType());
            }

            return bsonValue;
        }
    }
}
