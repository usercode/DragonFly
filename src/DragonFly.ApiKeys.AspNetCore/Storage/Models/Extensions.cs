using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Storage.Models;

static class Extensions
{
    public static MongoApiKey ToMongo(this ApiKey apiKey)
    {
        return new MongoApiKey()
        {
            Id = apiKey.Id,
            Name = apiKey.Name,
            Value = apiKey.Value,
            Permissions = apiKey.Permissions
        };
    }

    public static ApiKey ToModel(this MongoApiKey mongoApiKey)
    {
        return new ApiKey()
        {
            Id = mongoApiKey.Id,
            Name = mongoApiKey.Name,
            Value = mongoApiKey.Value,
            Permissions = mongoApiKey.Permissions
        };
    }
}
