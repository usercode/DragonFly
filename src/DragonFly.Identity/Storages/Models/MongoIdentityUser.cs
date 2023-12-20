// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore.Identity.MongoDB.Models;

class MongoIdentityUser : Entity<MongoIdentityUser>
{
    public string Username { get; set; }

    public string NormalizedUsername { get; set; }

    public string Email { get; set; }

    public string NormalizedEmail { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public IList<Guid> Roles { get; set; } = new List<Guid>();
}
