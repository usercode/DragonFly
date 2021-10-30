using DragonFly.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB
{
    /// <summary>
    /// MongoDbOptions
    /// </summary>
    public class MongoDbIdentityOptions
    {
        public MongoDbIdentityOptions()
        {
            Database = "DragonFly_Identity";
            Hostname = "localhost";

            InitialUsername = DefaultSecurity.DefaultUsername;
            InitialPassword = DefaultSecurity.DefaultPassword;
        }

        /// <summary>
        /// Database
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Hostname
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string? Password { get; set; }

        public string InitialUsername { get; set; }

        public string InitialPassword { get; set; }
    }
}
