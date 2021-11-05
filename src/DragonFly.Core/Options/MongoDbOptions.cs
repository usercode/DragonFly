using DragonFly.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB.Options
{
    /// <summary>
    /// MongoDbOptions
    /// </summary>
    public class MongoDbOptions
    {
        public MongoDbOptions()
        {
            Database = "DragonFly_App";
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

        /// <summary>
        /// InitialUsername
        /// </summary>
        public string InitialUsername { get; set; }

        /// <summary>
        /// InitialPassword
        /// </summary>
        public string InitialPassword { get; set; }
    }
}
