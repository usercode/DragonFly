﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Storage.Abstractions;
using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// ArrayMongoFieldSerializer
/// </summary>
public class ArrayMongoFieldSerializer : MongoFieldSerializer<ArrayField>
{
    public override ArrayField Read(SchemaField schemaField, BsonValue bsonvalue)
    {
        ArrayField contentField = new ArrayField();

        if (bsonvalue is BsonArray bsonArray)
        {
            ArrayFieldOptions? arrayOptions = (ArrayFieldOptions?)schemaField.Options;

            if (arrayOptions == null)
            {
                throw new Exception("arrayfield options are not available.");
            }

            foreach (BsonDocument item in bsonArray)
            {
                ArrayFieldItem arrayFieldItem = arrayOptions.CreateArrayItem();

                foreach (BsonElement subitem in item)
                {
                    subitem.Value.ToModelValue(subitem.Name, arrayFieldItem, arrayOptions);
                }

                contentField.Items.Add(arrayFieldItem);
            }
        }

        return contentField;
    }

    public override BsonValue Write(ArrayField contentField)
    {
        BsonArray bsonArray = new BsonArray();

        foreach (ArrayFieldItem item in contentField.Items)
        {
            BsonDocument doc = new BsonDocument();

            foreach (var f in item.Fields)
            {
                doc.Add(f.Key, f.Value.ToBsonValue());
            }

            bsonArray.Add(doc);
        }

        return bsonArray;
    }
}
