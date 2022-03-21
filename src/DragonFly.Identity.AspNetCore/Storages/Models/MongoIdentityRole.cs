using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Models;

class MongoIdentityRole : Entity
{
    public MongoIdentityRole()
    {
        Permissions = new List<string>();
    }

    public string Name { get; set; }

    public IList<string> Permissions { get; set; }
}
