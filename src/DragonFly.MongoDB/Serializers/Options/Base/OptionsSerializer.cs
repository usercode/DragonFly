// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// OptionsSerializer
/// </summary>
public abstract class OptionsSerializer<TOptions> : IOptionsSerializer
    where TOptions : FieldOptions
{
    public Type OptionsType => typeof(TOptions);

    public abstract TOptions Read(BsonValue bsonValue);
    
    public abstract BsonValue Write(TOptions options);

    BsonValue IOptionsSerializer.Write(FieldOptions options)
    {
        return Write((TOptions)options);
    }

    FieldOptions IOptionsSerializer.Read(BsonValue bsonvalue)
    {
        return Read(bsonvalue);
    }
}
