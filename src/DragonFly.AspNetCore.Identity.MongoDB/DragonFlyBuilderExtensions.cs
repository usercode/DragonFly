using AspNetCore.Identity.Mongo;
using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Core.Builders;
using DragonFly.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddIdentityMongoDb(this IDragonFlyBuilder builder, Action<MongoIdentityOptions> dbOptions)
        {
            builder.Services.AddTransient<ILoginService, LoginService>();

            builder.AddIdentity<DbUser, DbRole>(x => x.AddMongoDbStores<DbUser, DbRole, Guid>(dbOptions));
            
            return builder;
        }
    }
}
