using AspNetCore.Identity.Mongo.Model;
using DragonFly.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Models
{
    class DbUser : MongoUser<Guid>, IDragonFlyUser
    {
        public DbUser()
        {
        }
    }
}
