// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// IOptionsSerializer
/// </summary>
public interface IOptionsSerializer
{
    Type OptionsType { get; }

    FieldOptions Read(BsonValue bsonvalue);
    
    BsonValue Write(FieldOptions options);
}
