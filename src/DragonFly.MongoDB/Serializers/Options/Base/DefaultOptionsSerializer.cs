// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace DragonFly.MongoDB;

/// <summary>
/// DefaultOptionsSerializer
/// </summary>
internal class DefaultOptionsSerializer<TOptions> : OptionsSerializer<TOptions>
    where TOptions : ContentFieldOptions, new()
{
    public override TOptions Read(BsonValue bsonValue)
    {
        if (bsonValue is BsonDocument bsonDocument)
        {
            return BsonSerializer.Deserialize<TOptions>(bsonDocument);
        }

        return new TOptions();
    }

    public override BsonValue Write(TOptions options)
    {
        return options.ToBsonDocument(options.GetType());
    }
}
