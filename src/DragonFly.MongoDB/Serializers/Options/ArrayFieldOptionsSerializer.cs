// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// ArrayFieldOptionsSerializer
/// </summary>
public class ArrayFieldOptionsSerializer : OptionsSerializer<ArrayFieldOptions>
{
    public override ArrayFieldOptions Read(BsonValue bsonValue)
    {
        ArrayFieldOptions options = new ArrayFieldOptions();

        if (bsonValue is BsonDocument bsonDocument)
        {

        }

        return options;
    }

    public override BsonValue Write(ArrayFieldOptions options)
    {
        return options.ToMongo();
    }
}
