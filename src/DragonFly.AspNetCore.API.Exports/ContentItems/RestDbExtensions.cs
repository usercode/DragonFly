using DragonFly.AspNetCore.API.Exports.Json;
using DragonFly.AspNetCore.API.Models.Assets;
using DragonFly.Client;
using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.Data.Models;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.Models
{
    public static class RestDbExtensions
    {
        public static void ToModelValue(this JsonNode? jsonNode, string fieldName, IContentElement content, ISchemaElement schema)
        {
            if (jsonNode == null)
            {
                return;
            }

            if (content.Fields.TryGetValue(fieldName, out ContentField? contentField))
            {
                //value
                if (jsonNode is JsonValue jsonValue)
                {
                    if (contentField is SingleValueContentField<string> stringField)
                    {
                        if (jsonValue.TryGetValue(out string? stringValue))
                        {
                            stringField.Value = stringValue;
                        }
                    }
                    else if (contentField is SingleValueContentField<bool?> boolField)
                    {
                        if (jsonValue.TryGetValue(out bool? boolValue))
                        {
                            boolField.Value = boolValue;
                        }
                    }
                    else if (contentField is SingleValueContentField<Guid?> guidField)
                    {
                        if (jsonValue.TryGetValue(out Guid? guidValue))
                        {
                            guidField.Value = guidValue;
                        }
                    }
                    else if (contentField is SingleValueContentField<long?> longField)
                    {
                        if (jsonValue.TryGetValue(out long? longValue))
                        {
                            longField.Value = longValue;
                        }
                    }
                    else if (contentField is SingleValueContentField<int?> intField)
                    {
                        if (jsonValue.TryGetValue(out int? intValue))
                        {
                            intField.Value = intValue;
                        }
                    }
                    else if (contentField is SingleValueContentField<short?> shortField)
                    {
                        if (jsonValue.TryGetValue(out short? shortValue))
                        {
                            shortField.Value = shortValue;
                        }
                    }
                    else if (contentField is SingleValueContentField<float?> floatField)
                    {
                        if (jsonValue.TryGetValue(out float? floatValue))
                        {
                            floatField.Value = floatValue;
                        }
                    }
                    else if (contentField is SingleValueContentField<double?> doubleField)
                    {
                        if (jsonValue.TryGetValue(out double? doubleValue))
                        {
                            doubleField.Value = doubleValue;
                        }
                    }
                }
                //object
                else if (jsonNode is JsonObject jsonObject)
                {
                    if (contentField is ReferenceField referenceField)
                    {
                        if (jsonObject.Count > 0)
                        {
                            RestContentItem? restContentItem = jsonObject.Deserialize<RestContentItem>();

                            if (restContentItem != null)
                            {
                                referenceField.ContentItem = restContentItem.ToModel();
                            }
                        }
                    }
                    else if (contentField is EmbedField embedField)
                    {
                        RestContentEmbedded restContentItem = jsonObject.GetValue<RestContentEmbedded>();
                        embedField.ContentEmbedded = restContentItem.ToModel();
                    }
                    else if (contentField is AssetField assetField)
                    {
                        if (jsonObject.Count > 0)
                        {
                            RestAsset? restAsset = jsonObject.Deserialize<RestAsset>(JsonSerializerDefault.Options);
                            
                            if (restAsset != null)
                            {
                                assetField.Asset = restAsset.ToModel();
                            }
                        }
                    }
                    else
                    {
                        ContentField? c = (ContentField?)jsonObject.Deserialize(contentField.GetType(), JsonSerializerDefault.Options);

                        if (c != null)
                        {
                            content.Fields[fieldName] = c;
                        }
                    }
                }
                //array
                else if (jsonNode is JsonArray jsonArray)
                {
                    if (contentField is ArrayField arrayField)
                    {
                        if (schema.Fields.TryGetValue(fieldName, out SchemaField? definition))
                        {
                            ArrayFieldOptions? arrayOptions = definition.Options as ArrayFieldOptions;

                            if (arrayOptions == null)
                            {
                                throw new Exception();
                            }

                            foreach (JsonNode? item in jsonArray)
                            {
                                if (item is JsonObject itemJsonObject)
                                {
                                    ArrayFieldItem arrayFieldItem = arrayOptions.CreateArrayField();

                                    foreach (var subitem in itemJsonObject)
                                    {
                                        ToModelValue(subitem.Value, subitem.Key, arrayFieldItem, arrayOptions);
                                    }

                                    arrayField.Items.Add(arrayFieldItem);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static JsonNode? ToRestValue(this ContentField field)
        {
            return ToRestValue(field, true);
        }

        public static JsonNode? ToRestValue(this ContentField field, bool includeNavigationProperties)
        {
            JsonNode? bsonValue = null;

            if (field is ISingleValueContentField singleValueField)
            {
                if (singleValueField.HasValue)
                {
                    bsonValue = JsonValue.Create(singleValueField.Value);
                }
            }
            else if (field is ReferenceField referenceField)
            {
                if (referenceField.ContentItem != null)
                {
                    if (includeNavigationProperties == true)
                    {
                        bsonValue = JsonSerializer.SerializeToNode(referenceField.ContentItem.ToRest(true, false), JsonSerializerDefault.Options);
                    }
                }
            }
            else if (field is EmbedField embedField)
            {
                if (embedField.ContentEmbedded != null)
                {
                    bsonValue = JsonSerializer.SerializeToNode(embedField.ContentEmbedded.ToRest(), JsonSerializerDefault.Options);
                }
            }
            else if (field is AssetField assetField)
            {
                if (assetField.Asset != null)
                {
                    bsonValue = JsonSerializer.SerializeToNode(assetField.Asset.ToRest(), JsonSerializerDefault.Options);
                }
            }
            else if (field is ArrayField arrayField)
            {
                JsonArray bsonArray = new JsonArray();

                foreach (ArrayFieldItem item in arrayField.Items)
                {
                    JsonObject doc = new JsonObject();

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
                bsonValue = JsonSerializer.SerializeToNode(field, field.GetType(), JsonSerializerDefault.Options);
            }

            return bsonValue;
        }
    }
}
