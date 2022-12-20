// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using System.Collections.Generic;

namespace DragonFLy.ApiKeys.AspNetCore.Storage.Models;

class MongoApiKey : Entity<MongoApiKey>
{
    public MongoApiKey()
    {
        Name = string.Empty;
        Value = string.Empty;
        Permissions = new List<string>();
    }

    public string Name { get; set; }

    public string Value { get; set; }

    public IList<string> Permissions { get; set; }
}
