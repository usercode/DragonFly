// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity;
using MongoDB.Driver;

namespace DragonFly.AspNetCore.Identity.MongoDB.Storages.Models;

static class Extensions
{
    public static MongoIdentityUser ToMongo(this IdentityUser user)
    {
        MongoIdentityUser mongoIdentityUser = new MongoIdentityUser();
        mongoIdentityUser.Id = user.Id;
        mongoIdentityUser.Username = user.Username;
        mongoIdentityUser.Email = user.Email;
        mongoIdentityUser.Roles = user.Roles.Select(x => x.Id).ToList();

        return mongoIdentityUser;
    }

    public static IdentityUser ToModel(this MongoIdentityUser user,  MongoIdentityStore store)
    {
        IdentityUser identityUser = new IdentityUser();
        identityUser.Id = user.Id;
        identityUser.Username = user.Username;
        identityUser.Email = user.Email;

        IList<IdentityRole> roles = store.Roles
                                            .AsQueryable()
                                            .Where(x => user.Roles.Contains(x.Id))
                                            .ToList()
                                            .Select(x=> x.ToModel())
                                            .ToList();

        identityUser.Roles = roles;

        return identityUser;
    }

    public static MongoIdentityRole ToMongo(this IdentityRole role)
    {
        MongoIdentityRole mongoIdentityRole = new MongoIdentityRole();
        mongoIdentityRole.Id = role.Id;
        mongoIdentityRole.Name = role.Name;
        mongoIdentityRole.Permissions = role.Permissions;

        return mongoIdentityRole;
    }

    public static IdentityRole ToModel(this MongoIdentityRole role)
    {
        IdentityRole identityRole = new IdentityRole();
        identityRole.Id = role.Id;
        identityRole.Name = role.Name;
        identityRole.Permissions = role.Permissions.ToList();

        return identityRole;
    }
}
