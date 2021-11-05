using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.MongoDB.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB
{
    /// <summary>
    /// MongIdentityStore
    /// </summary>
    internal class MongoIdentityStore
    {
        public MongoIdentityStore(IOptions<MongoDbOptions> options)
        {
            Options = options.Value;

            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(Options.Hostname);

            if (string.IsNullOrEmpty(Options.Username) == false)
            {
                settings.Credential = MongoCredential.CreateCredential("admin", Options.Username, Options.Password);
            }

            Client = new MongoClient(settings);

            Database = Client.GetDatabase(Options.Database);

            Users = Database.GetCollection<MongoIdentityUser>("Identity_Users");
            Roles = Database.GetCollection<MongoIdentityRole>("Identity_Roles");

            Users.Indexes.CreateOne(new CreateIndexModel<MongoIdentityUser>(Builders<MongoIdentityUser>.IndexKeys.Ascending(x => x.NormalizedEmail)));
            Users.Indexes.CreateOne(new CreateIndexModel<MongoIdentityUser>(Builders<MongoIdentityUser>.IndexKeys.Ascending(x => x.NormalizedUsername)));

            Roles.Indexes.CreateOne(new CreateIndexModel<MongoIdentityRole>(Builders<MongoIdentityRole>.IndexKeys.Ascending(x => x.Name)));

        }

        public MongoDbOptions Options { get; }

        public MongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public IMongoCollection<MongoIdentityUser> Users { get; }

        public IMongoCollection<MongoIdentityRole> Roles { get; }
    }
}
