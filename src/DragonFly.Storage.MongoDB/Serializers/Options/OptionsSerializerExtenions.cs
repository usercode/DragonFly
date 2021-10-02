﻿using DragonFly.Content;
using DragonFly.Data.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Serializers.Options
{
    /// <summary>
    /// OptionsSerializerExtenions
    /// </summary>
    public static class OptionsSerializerExtenions
    {
        public static BsonValue ToMongo(this ArrayFieldOptions options)
        {
            BsonDocument bsonDocument = new BsonDocument();

            bsonDocument.Add(nameof(ArrayFieldOptions.IsRequired), options.IsRequired);
            bsonDocument.Add(nameof(ArrayFieldOptions.IsSearchable), options.IsSearchable);
            bsonDocument.Add(nameof(ArrayFieldOptions.MinItems), options.MinItems);
            bsonDocument.Add(nameof(ArrayFieldOptions.MaxItems), options.MaxItems);

            BsonDocument bsonFields = new BsonDocument();

            foreach (var field in options.Fields)
            {
                bsonFields.Add(field.Key, field.Value.ToMongo().ToBsonDocument());
            }

            bsonDocument.Add(nameof(ArrayFieldOptions.Fields), bsonFields);

            return bsonDocument;
        }
    }
}
