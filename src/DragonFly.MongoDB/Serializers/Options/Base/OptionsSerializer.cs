// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// OptionsSerializer
/// </summary>
public abstract class OptionsSerializer<TOptions> : IOptionsSerializer
    where TOptions : ContentFieldOptions
{
    public Type OptionsType => typeof(TOptions);

    public abstract TOptions Read(BsonValue bsonValue);
    
    public abstract BsonValue Write(TOptions options);

    BsonValue IOptionsSerializer.Write(ContentFieldOptions options)
    {
        return Write((TOptions)options);
    }

    ContentFieldOptions IOptionsSerializer.Read(BsonValue bsonvalue)
    {
        return Read(bsonvalue);
    }
}
