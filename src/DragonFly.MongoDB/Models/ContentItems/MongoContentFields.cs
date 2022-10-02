// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
