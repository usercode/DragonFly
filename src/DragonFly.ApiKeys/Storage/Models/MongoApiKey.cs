// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.ApiKeys.AspNetCore.Storage.Models;

class MongoApiKey : Entity<MongoApiKey>
{
    public string Name { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public IList<string> Permissions { get; set; } = [];
}
