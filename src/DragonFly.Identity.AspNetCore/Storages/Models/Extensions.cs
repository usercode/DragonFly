﻿using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Storages.Models
{
    static class Extensions
    {
        public static MongoIdentityUser ToMongo(this IdentityUser user)
        {
            MongoIdentityUser mongoIdentityUser = new MongoIdentityUser();
            mongoIdentityUser.Id = user.Id;
            mongoIdentityUser.Username = user.UserName;
            mongoIdentityUser.Email = user.Email;
            mongoIdentityUser.Roles = user.Roles.Select(x => x.Id).ToList();

            return mongoIdentityUser;
        }

        public static IdentityUser ToModel(this MongoIdentityUser user,  MongoIdentityStore store)
        {
            IdentityUser identityUser = new IdentityUser();
            identityUser.Id = user.Id;
            identityUser.UserName = user.Username;
            identityUser.Email = user.Email;

            identityUser.Roles = user.Roles
                                        .Select(x => store.Roles.AsQueryable().FirstOrDefault(a => a.Id == x))
                                        .Where(x => x != null)
                                        .Select(x => x!.ToModel())
                                        .ToList();

            return identityUser;
        }

        public static MongoIdentityRole ToMongo(this IdentityRole role)
        {
            MongoIdentityRole mongoIdentityRole = new MongoIdentityRole();
            mongoIdentityRole.Id = role.Id;
            mongoIdentityRole.Name = role.Name;

            return mongoIdentityRole;
        }

        public static IdentityRole ToModel(this MongoIdentityRole role)
        {
            IdentityRole identityRole = new IdentityRole();
            identityRole.Id = role.Id;
            identityRole.Name = role.Name;
            //identityRole.Permissions = role.Permissions.ToList();

            return identityRole;
        }
    }
}