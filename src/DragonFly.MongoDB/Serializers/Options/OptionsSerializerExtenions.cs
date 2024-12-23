// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// OptionsSerializerExtenions
/// </summary>
public static class OptionsSerializerExtenions
{
    public static BsonValue ToMongo(this ArrayFieldOptions options)
    {
        BsonDocument bsonDocument = new BsonDocument
        {
            { nameof(ArrayFieldOptions.IsRequired), options.IsRequired },
            { nameof(ArrayFieldOptions.HasIndex), options.HasIndex },
            { nameof(ArrayFieldOptions.MinItems), options.MinItems },
            { nameof(ArrayFieldOptions.MaxItems), options.MaxItems }
        };

        BsonDocument bsonFields = new BsonDocument();

        foreach (var field in options.Fields)
        {
            bsonFields.Add(field.Key, field.Value.ToMongo().ToBsonDocument());
        }

        bsonDocument.Add(nameof(ArrayFieldOptions.Fields), bsonFields);

        return bsonDocument;
    }
}
