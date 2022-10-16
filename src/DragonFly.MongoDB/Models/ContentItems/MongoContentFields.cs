// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// ContentField
/// </summary>
public class MongoContentFields : Dictionary<string, BsonValue>
{
    public MongoContentFields()
    {

    }
}
