using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys.AspNetCore.Storage.Models;

class MongoApiKey : Entity
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
