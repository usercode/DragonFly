// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
